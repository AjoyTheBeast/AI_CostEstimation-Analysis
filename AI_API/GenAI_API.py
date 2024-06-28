import pandas as pd
from sqlalchemy import create_engine
from fastapi import FastAPI
from pydantic import BaseModel
import json
from IPython.display import display
from IPython.display import Markdown
import textwrap
import google.generativeai as genai
import os
from langchain.text_splitter import RecursiveCharacterTextSplitter
from langchain_google_genai import GoogleGenerativeAIEmbeddings
from langchain import PromptTemplate
import re

genai.configure(api_key="AIzaSyBAmiLQpQunz3aBgzDY9jjOwBW6jFuDp00")
os.environ["GOOGLE_API_KEY"] = "AIzaSyBAmiLQpQunz3aBgzDY9jjOwBW6jFuDp00"
HARIS_API = os.environ.get('GOOGLE_API_KEY')


class SqlConnection:
    def __init__(self, server='471D0A8AE4E15D5\MSSQLSERVER01', database='AICostEstimation',
                 driver='ODBC Driver 17 for SQL Server'):
        self.server = server
        self.database = database
        self.driver = driver

        self.connection_string = f'mssql+pyodbc://@{self.server}/{self.database}?driver={self.driver}'
        self.engine = create_engine(self.connection_string)
        self.con = self.engine.connect()

    def sql_data(self):
        query = "SELECT * FROM [dbo].[Dataset]"
        pd.set_option('display.max_columns', None)
        pd.set_option('display.max_rows', None)
        data = pd.read_sql_query(query, self.con)
        return data

    def sql_insert(self, CliRe,res):
        if res.count(":")==2:
            v=valuextract(res)
            client_data = pd.DataFrame({'ClientRequirements': [CliRe],
                                        'Project_Duration': str(v[1]),
                                        'Project_Cost': str(v[0])})
            client_data.to_sql('Dataset', self.con, if_exists='append', index=False)
            self.con.commit()


def load_and_split(data, page_size=1):
    pages = []
    for i in range(0, len(data), page_size):
        pages.append(data.iloc[i:i + page_size])
    return pages


def texts_splitter(data):
    text_splitter = RecursiveCharacterTextSplitter(chunk_size=10000, chunk_overlap=1000)
    pages = load_and_split(data.copy())
    context = "\n\n".join(str(p) for p in pages)
    texts = text_splitter.split_text(context)
    return texts


def to_markdown(text):
    text = text.replace('•', '  *')
    return Markdown(textwrap.indent(text, '> ', predicate=lambda _: True))


def valuextract(res):
    regex = r"\d+"
    v = re.findall(regex, res)
    return v


embeddings = GoogleGenerativeAIEmbeddings(model="models/embedding-001", google_api_key=HARIS_API)

#no estimation found
prompt_template = """You have a dataset containing information about project descriptions along with their corresponding project costs and durations. Given a project description, predict the project duration and cost using the dataset data find the matches of words from question and dataset and get the average of the selected project cost and project duration. If the input is  gibberish, respond with ‘Please provide more information.’ Otherwise, provide the output in the format: ‘Project duration: [duration] days’ and ‘Project cost = $[cost].’" \n \n
                    dataset: \n {dataset}?\n
                    Question: \n {question} \n
                    Answer:
                  """

'''
prompt_template = """**Project Analysis**

**Context:**

{context}

**Question:**

{question}

**Analysis:**

Based on the provided information about ProjectID, ProjectComplexity, TechnologyStack, TeamSize, DevelopmentMethodology, ProjectDomain, ProjectTimeline, RiskFactors, ClientRequirements, QualityMetrics, and ExternalFactors, here's an analysis to determine the project cost and duration:

* **Project Cost (in INR):** This will depend on factors like project complexity, technology stack, team size, and development methodology. A more complex project with a specialized stack and larger team will generally cost more.

* **Project Duration:** This is influenced by project complexity, team size, development methodology, and potential risk factors. Complex projects with larger teams or agile methodologies might have shorter timelines compared to less complex projects with smaller teams and traditional methodologies.

**Answer:**

**Project Cost:** (Estimated based on analysis)

**Project Duration:** (Estimated based on analysis)

**Note:**

- If the question is unclear or lacks sufficient context, the answer may be: "Please provide more specific information for a more accurate estimate."
- If the question is irrelevant to project cost or duration, the answer may be: "This question doesn't directly relate to project cost or duration. Please rephrase your question if you have one about those aspects."
"""
'''

prompt = PromptTemplate(
    template=prompt_template, input_variables=["context", "question"]
)
connection = SqlConnection()


def get_output(query):
    model = genai.GenerativeModel(model_name="gemini-pro")
    responses = model.generate_content(prompt.format(dataset=texts_splitter(connection.sql_data()), question=(query)))
    print(responses.text)
    connection.sql_insert( query,responses.text)
    return responses.text


class input(BaseModel):
    UserInput: str


app = FastAPI()


@app.post("/predictexp")
def predict_model(D: input):
    print(D.UserInput)
    res = get_output(D.UserInput)
    if res.count(":") == 2:
        output = {
            "MLOutput": res
        }
    else:
        output = {
            "MLOutput": res
        }
    return output

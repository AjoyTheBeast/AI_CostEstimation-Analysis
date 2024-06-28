import streamlit as st
import google.generativeai as genai
import os

def get_output(query):
    model = genai.GenerativeModel(model_name="gemini-pro")
    responses = model.generate_content(query)
    print(responses.text)
    return responses.text
# Main app
def main():
    st. title("GenAI TEST TOOL")

    # Initialize chat Interface
    user_input = st.text_input("Your Message:")
    print(user_input)
    if user_input:
        response = get_output(user_input)
        st.write(response)


if __name__ == "__main__":
    main()


genai.configure(api_key="AIzaSyBAmiLQpQunz3aBgzDY9jjOwBW6jFuDp00")



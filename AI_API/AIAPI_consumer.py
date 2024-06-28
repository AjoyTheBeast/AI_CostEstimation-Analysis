import requests
import json


data = json.dumps({
    "UserInput" : "Create an API using Python and Oracle to integrate it with front-end using agile approach"
})

response = requests.post("http://127.0.01:8000/predictexp", data=data)

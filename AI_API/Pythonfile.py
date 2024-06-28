import os
import streamlit as st
import tempfile
from GenAi_API import *


# Main app
def main():
    st. title("AI Cost Estimation TOOL")

    # Initialize chat Interface
    user_input = st.text_input("Your Message:")
    print(user_input)
    if user_input:
        response = get_output(user_input)
        st.write(response)


if __name__ == "__main__":
    main()

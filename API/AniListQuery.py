﻿import sys
import requests
import json

query = '''
query ($page: Int, $perPage: Int) {
    Page(page: $page, perPage: $perPage) {
      pageInfo {
        total
        perPage
      }
      media(type: ANIME, sort: FAVOURITES_DESC) {
        title {
          romaji
          english
          native
        }
        averageScore
        seasonYear
        episodes
      }
    }
  }
'''
   
variables = {
   'page': 1,
   'perPage': 50,
};

url = 'https://graphql.anilist.co'

# Make the HTTP Api request
response = requests.post(url, json={'query': query, 'variables': variables})

print(response.text)
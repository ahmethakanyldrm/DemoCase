# Demo Case


### Endpoints

#### Create Document
````
POST /api/Document
https://localhost:44345/api/Document
Content-Type: application/json
   {
  "id": 0,
  "path": "string",
  "name": "string"
}
````
#### Delete Document

````
DELETE /api/Document 
https://localhost:44345/api/Document/{id}

````

#### Get Licenses

````
GET /api/License
https://localhost:44345/api/License
[
  {
    "id": 1,
    "licenseKey": "abc",
    "expirationDate": "24-02"
  },
  {
    "id": 2,
    "licenseKey": "License Key 1234",
    "expirationDate": "25-02-2024"
  }
]
````

#### Get License

````
GET /api/License/1
https://localhost:44345/api/License/1

  {
    "id": 1,
    "licenseKey": "abc",
    "expirationDate": "24-02"
  }
 

````

#### Create License

````
POST /api/License
https://localhost:44345/api/License

 {
  "id": 0,
  "licenseKey": "string",
  "expirationDate": "string"
}

````

#### Update License

````
PUT /api/License/{id}
https://localhost:44345/api/License/{id}

 {
  "id": 1,
  "licenseKey": "update",
  "expirationDate": "update"
}

````

#### Delete License

````
DELETE /api/License/{id}
https://localhost:44345/api/License/{id}

````

# ValuationApi

It is a .net core api that valuate vessels according to their type and coefficients.

Techs:
- .Net 7.0
- ORM: Entity Framework
- Caching: MemoryCache
- Fluent Validation
- Dependency Injection
- Swagger
- Test: XUnit

Developed for two kind of roles, admin and client.

For Admin: There are operations for admin to add or update and get Vessels and Coefficients.
<img width="1437" alt="AdminOperations" src="https://github.com/muhammedkorkmaz/ValuationApi/assets/44212848/9fd4d239-7b2f-4805-a638-02df2427ab75">

To run the valuation some datas should be inserted. Below datas can be used for vessels and coefficients.

Coefficients ==> Post Body :

[
  {
    "type": "Fair Market Value",
    "a": 0.11,
    "b": 0.001,
    "constant": 3
  },
  {
    "type": "Operating Costs",
    "a": 1.2,
    "b": 0.21,
    "constant": 4
  },
  {
    "type": "Scrap Price",
    "a": 0.13,
    "b": 0.002,
    "constant": 5
  }
]

Vessels ==> Post Body: 

Dry Bulk:
{
  "imo": "IMO1234567",
  "vesselType": 0,
  "yearOfBuild": 2022,
  "size": 27000
}

Oil Thanker:
{
  "imo": "IMO7654321",
  "vesselType": 1,
  "yearOfBuild": 2016,
  "size": 36000
}

Container Ship:
{
  "imo": "IMO1234789",
  "vesselType": 2,
  "yearOfBuild": 2018,
  "size": 21000
}

- To valuate the vessel there is post method that take string array. Can be used for single or multiple entry. Valuate the vessel acording to given coefiicients and keep on memory cache 10 minutes for future requests.
- To get all active valuations, get method will be used as shown below.
<img width="1437" alt="Valuation" src="https://github.com/muhammedkorkmaz/ValuationApi/assets/44212848/d988a349-de5c-4e7e-b89d-1917540a0e02">



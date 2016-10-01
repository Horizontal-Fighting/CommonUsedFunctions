# IO.Swagger.Api.DataSetApi

All URIs are relative to *http://vautointerview.azurewebsites.net*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DataSetGetCheat**](DataSetApi.md#datasetgetcheat) | **GET** /api/{datasetId}/cheat | Get correct answer for dataset (cheat)
[**DataSetGetDataSetId**](DataSetApi.md#datasetgetdatasetid) | **GET** /api/datasetId | Creates new dataset and returns its ID
[**DataSetPostAnswer**](DataSetApi.md#datasetpostanswer) | **POST** /api/{datasetId}/answer | Submit answer for dataset


<a name="datasetgetcheat"></a>
# **DataSetGetCheat**
> Answer DataSetGetCheat (string datasetId)

Get correct answer for dataset (cheat)

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DataSetGetCheatExample
    {
        public void main()
        {
            
            var apiInstance = new DataSetApi();
            var datasetId = datasetId_example;  // string | 

            try
            {
                // Get correct answer for dataset (cheat)
                Answer result = apiInstance.DataSetGetCheat(datasetId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DataSetApi.DataSetGetCheat: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **datasetId** | **string**|  | 

### Return type

[**Answer**](Answer.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datasetgetdatasetid"></a>
# **DataSetGetDataSetId**
> DatasetIdResponse DataSetGetDataSetId ()

Creates new dataset and returns its ID

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DataSetGetDataSetIdExample
    {
        public void main()
        {
            
            var apiInstance = new DataSetApi();

            try
            {
                // Creates new dataset and returns its ID
                DatasetIdResponse result = apiInstance.DataSetGetDataSetId();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DataSetApi.DataSetGetDataSetId: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**DatasetIdResponse**](DatasetIdResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="datasetpostanswer"></a>
# **DataSetPostAnswer**
> AnswerResponse DataSetPostAnswer (string datasetId, Answer answer)

Submit answer for dataset

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DataSetPostAnswerExample
    {
        public void main()
        {
            
            var apiInstance = new DataSetApi();
            var datasetId = datasetId_example;  // string | 
            var answer = new Answer(); // Answer | 

            try
            {
                // Submit answer for dataset
                AnswerResponse result = apiInstance.DataSetPostAnswer(datasetId, answer);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DataSetApi.DataSetPostAnswer: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **datasetId** | **string**|  | 
 **answer** | [**Answer**](Answer.md)|  | 

### Return type

[**AnswerResponse**](AnswerResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, text/html, application/xml, text/xml, application/x-www-form-urlencoded
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


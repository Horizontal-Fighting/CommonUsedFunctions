# IO.Swagger.Api.DealersApi

All URIs are relative to *http://vautointerview.azurewebsites.net*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DealersGetDealer**](DealersApi.md#dealersgetdealer) | **GET** /api/{datasetId}/dealers/{dealerid} | Get information about a dealer


<a name="dealersgetdealer"></a>
# **DealersGetDealer**
> DealersResponse DealersGetDealer (string datasetId, int? dealerid)

Get information about a dealer

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DealersGetDealerExample
    {
        public void main()
        {
            
            var apiInstance = new DealersApi();
            var datasetId = datasetId_example;  // string | 
            var dealerid = 56;  // int? | 

            try
            {
                // Get information about a dealer
                DealersResponse result = apiInstance.DealersGetDealer(datasetId, dealerid);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DealersApi.DealersGetDealer: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **datasetId** | **string**|  | 
 **dealerid** | **int?**|  | 

### Return type

[**DealersResponse**](DealersResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


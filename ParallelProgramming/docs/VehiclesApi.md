# IO.Swagger.Api.VehiclesApi

All URIs are relative to *http://vautointerview.azurewebsites.net*

Method | HTTP request | Description
------------- | ------------- | -------------
[**VehiclesGetIds**](VehiclesApi.md#vehiclesgetids) | **GET** /api/{datasetId}/vehicles | Get a list of all vehicleids in dataset
[**VehiclesGetVehicle**](VehiclesApi.md#vehiclesgetvehicle) | **GET** /api/{datasetId}/vehicles/{vehicleid} | Get information about a vehicle


<a name="vehiclesgetids"></a>
# **VehiclesGetIds**
> VehicleIdsResponse VehiclesGetIds (string datasetId)

Get a list of all vehicleids in dataset

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class VehiclesGetIdsExample
    {
        public void main()
        {
            
            var apiInstance = new VehiclesApi();
            var datasetId = datasetId_example;  // string | 

            try
            {
                // Get a list of all vehicleids in dataset
                VehicleIdsResponse result = apiInstance.VehiclesGetIds(datasetId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling VehiclesApi.VehiclesGetIds: " + e.Message );
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

[**VehicleIdsResponse**](VehicleIdsResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="vehiclesgetvehicle"></a>
# **VehiclesGetVehicle**
> VehicleResponse VehiclesGetVehicle (string datasetId, int? vehicleid)

Get information about a vehicle

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class VehiclesGetVehicleExample
    {
        public void main()
        {
            
            var apiInstance = new VehiclesApi();
            var datasetId = datasetId_example;  // string | 
            var vehicleid = 56;  // int? | 

            try
            {
                // Get information about a vehicle
                VehicleResponse result = apiInstance.VehiclesGetVehicle(datasetId, vehicleid);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling VehiclesApi.VehiclesGetVehicle: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **datasetId** | **string**|  | 
 **vehicleid** | **int?**|  | 

### Return type

[**VehicleResponse**](VehicleResponse.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, text/json, text/html, application/xml, text/xml

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)


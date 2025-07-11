public class ApiResponse<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    )]
    public T? Data { get; set; }

    [System.Text.Json.Serialization.JsonIgnore(
        Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    )]
    public string? RequestId { get; set; }

    public ApiResponse() { }

    public ApiResponse(int statusCode, string? message, T? data = default, string? requestId = null)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
        RequestId = requestId;
    }
}

# Define the API endpoint and token
$apiUrl = "http://localhost:5222/api/AddressEntries"
$token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmVoYWQgSGFzaGlzaCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6IkplaGFkLmhhc2hpc2guMTk5OUBnbWFpbC5jb20iLCJleHAiOjE3Mzc0MTg1NjMsImlzcyI6IkFkZHJlc3NCb29rIiwiYXVkIjoiQWRkcmVzc0Jvb2sifQ.C53JcCTe7g9erLLEKjGcn6kFddnWDz_xVmmA-MXBVtQ"

# Define the payload
$payload = @{
    fullName = "gggggn"
    jobId = 1
    departmentId = 1
    mobileNumber = "0123333332"
    dateOfBirth = "2002-01-18"
    email = "smming@gmail.com"
    address = "22 hydepark"
} | ConvertTo-Json -Depth 10

# Define the headers
$headers = @{
    "Content-Type" = "application/json"
    "Authorization" = "Bearer $token"
}

# Send the POST request
$response = Invoke-RestMethod -Uri $apiUrl -Method Post -Headers $headers -Body $payload

# Output the response
$response

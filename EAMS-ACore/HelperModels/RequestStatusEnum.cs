using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMS_ACore.HelperModels
{
    public enum RequestStatusEnum
    {
        // The request was successful, and the server has fulfilled it.
        OK = 200,

        // The request was successful, and a new resource was created as a result.
        Created = 201,

        // The request was successful, but there is no additional content to send in the response.
        NoContent = 204,

        // The server has received the request, but the client has provided invalid data.
        BadRequest = 400,

        // The requested resource could not be found on the server.
        NotFound = 404,

        // The server understands the request, but it refuses to authorize it.
        Unauthorized = 401,

        // The server understands the request, but the client must authenticate itself to get the requested response.
        Forbidden = 403,
         

        // The server encountered an unexpected condition that prevented it from fulfilling the request.
        InternalServerError = 500,

        // The server does not support the functionality required to fulfill the request.
        NotImplemented = 501,

        
        // The server is currently unable to handle the request due to temporary overloading or maintenance of the server.
        ServiceUnavailable = 503,

        
    }

}

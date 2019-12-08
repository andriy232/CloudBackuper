using Google.Apis.Drive.v3;

namespace GDriveProvider
{
    public static class RequestExt
    {
        public static FilesResource.GetRequest SetFields(this FilesResource.GetRequest request)
        {
            if (request != null)
                request.Fields = "*";
            return request;
        }

        public static FilesResource.ListRequest SetFields(this FilesResource.ListRequest request)
        {
            if (request != null)
                request.Fields = "*";
            return request;
        }
    }
}
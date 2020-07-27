//using System.Net;
//using System.Net.Http;


//namespace Demo.Common
//{
//    public class Response
//    {
//        public void ResponseNotFound(string message)
//        {
//            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
//            {
//                Content = new StringContent(string.Format($"{message}")),
//                ReasonPhrase="object is not found"
//            };
//            throw new HttpResponseException(resp);
//        }
//        public void ResponseBadRequest(string message)
//        {
//            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
//            {
//                Content = new StringContent(string.Format($"{message}")),
//                ReasonPhrase = "object is badRequest"
//            };
//            throw new HttpResponseException(resp);
//        }
//    }
//}

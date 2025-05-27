using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Response<T>        //La T dic e que puede llegar una taboa, objeto, modelo, lo que sea ahi lo va a guardar porque yo lo mande ahi, sirve para que se lea en el lado del front
    {
        public Response() { }

        public Response(T data, string message = null)  //el constructor puede mandar esto o
        {
            Succeded = true;
            Message = message;
            Result = data;
        }

        public Response(string message)         //solo este mensaje
        {
            Succeded = false;
            Message = message;
        }
            //por si no lo recuerda son las delcaraciones pero abajo
        public bool Succeded { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}

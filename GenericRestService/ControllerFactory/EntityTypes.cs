using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

using GenericRestService.Requests;
using GenericRestService.Responses;
using Data.entities;

namespace GenericRestService.ControllerFactory
{
    public static class EntityTypes
    {
        public static Dictionary<TypeInfo, List<TypeInfo>> model_types => new Dictionary<TypeInfo, List<TypeInfo>>()
        {
            { typeof(Albums).GetTypeInfo(), new List<TypeInfo>() { typeof(AlbumRequest).GetTypeInfo(), typeof(AlbumResponse).GetTypeInfo() } }
        };
    }
}
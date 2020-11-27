using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Series.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Series.Help
{
    public  class ConvertObject
    {
        private static ConvertObject instance;
        private ConvertObject()
        {

        }
        public static ConvertObject Instance
        {
            get
            {
                if (instance == null) instance = new ConvertObject();
                return instance;
            }
        }
        public  ActorCreate CreateActorCreate(ActorAdd actor)
        {

            try
            {
                return new ActorCreate()
                {
                    ActorId = actor.ActorId,
                    Biography = actor.Biography,
                    ImageUrl = actor.ImageUrl,
                    Name = actor.Name,
                    Surname = actor.Surname,
                    WikiUrl = actor.WikiUrl

                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public  ActorAdd CreateActorAdd(ActorCreate actor)
        {
            try
            {
                return new ActorAdd()
                {
                    ActorId = actor.ActorId,
                    Biography = actor.Biography,
                    ImageUrl = actor.ImageUrl,
                    Name = actor.Name,
                    Surname = actor.Surname,
                    WikiUrl = actor.WikiUrl
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

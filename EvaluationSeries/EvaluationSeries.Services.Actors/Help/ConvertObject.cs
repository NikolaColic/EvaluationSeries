using EvaluationSeries.Grpc;
using EvaluationSeries.Services.Actors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluationSeries.Services.Actors.Help
{
    public class ConvertObject
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
        public Actor CreateActor(ActorAdd actor)
        {

            try
            {
                return new Actor()
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
        public ActorAdd CreateActorAdd(Actor actor)
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

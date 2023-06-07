using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using PiperBotTI.Contracts;

namespace PiperBotTI.Infraestructure.Luis
{
    public class LuisService : ILuisService 
    {
        public LuisRecognizer _luisRecognizer { get; set; }

        public LuisService(IConfiguration configuration)
        {
            var luisApplication = new LuisApplication(
                configuration["LuisApiKey"]
                );

            var recognizerOptions = new LuisRecognizerOptionsV3(luisApplication)
            {
                PredictionOptions = new Microsoft.Bot.Builder.AI.LuisV3.LuisPredictionOptions()
                {
                    IncludeInstanceData = true
                }
            };
            _luisRecognizer = new LuisRecognizer(recognizerOptions);
        }

     
    }
}

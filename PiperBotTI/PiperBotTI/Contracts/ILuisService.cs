using Microsoft.Bot.Builder.AI.Luis;

namespace PiperBotTI.Contracts
{
    public interface ILuisService
    {
        LuisRecognizer _luisRecognizer { get;set; }
    }
}

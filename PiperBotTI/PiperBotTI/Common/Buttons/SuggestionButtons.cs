using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PiperBotTI.Common.Buttons
{
    public class SuggestionButtons
    {
         public static async Task<DialogTurnResult>ShowOptions(WaterfallStepContext stepContext,CancellationToken cancellationToken)
         {
            var options = await stepContext.PromptAsync(
              nameof(TextPrompt),
              new PromptOptions
              {
                  Prompt = CreateSuggestions()
              },
              cancellationToken);
            return options;
         }

        
            

        private static Activity CreateSuggestions()
        {
            var reply = MessageFactory.Text("Elige una opción por favor");

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                {
                    new CardAction(){Title = "Necesito ayuda", Value = "Necesito ayuda", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Sobre Kpmg", Value = "Necesito ayuda", Type = ActionTypes.ImBack},
                    new CardAction(){Title = "Sobre Tecnologia", Value = "Necesito ayuda", Type = ActionTypes.ImBack}
                }
            };

            return reply;
        }
    }
}

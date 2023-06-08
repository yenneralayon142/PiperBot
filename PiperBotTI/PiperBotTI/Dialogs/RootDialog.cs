using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using PiperBotTI.Common.Cards;
using PiperBotTI.Contracts;
using PiperBotTI.Dialogs.Qualification;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PiperBotTI.Dialogs
{
    public class RootDialog : ComponentDialog
    {
        private readonly ILuisService _luisService;

        public RootDialog(ILuisService luisService)
        {
            _luisService = luisService;
            var waterfallSteps = new WaterfallStep[]
            {
                InitialProccess,
                FinalProccess
            };
            AddDialog(new QualificatioDialog());
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> InitialProccess(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
           var luisResult = await _luisService._luisRecognizer.RecognizeAsync(stepContext.Context,cancellationToken);
           return await ManageIntentions(stepContext, luisResult, cancellationToken);
        }

        private async Task<DialogTurnResult> ManageIntentions(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            var topIntent = luisResult.GetTopScoringIntent();
            switch (topIntent.intent)
            {
                case "Saludos":
                    await IntentSaludos(stepContext, luisResult, cancellationToken);
                    break;
                case "Despedirse":
                    await IntentDespedirse(stepContext,luisResult,cancellationToken);
                    break;
                case "Agradecer":
                    await IntentAgradecer(stepContext,luisResult,cancellationToken);
                    break;
                case "Calificar":
                   return await IntentCalificar(stepContext,luisResult,cancellationToken);
                    break;
                case "MesaAyuda":
                    await IntentMesaAyuda(stepContext,luisResult,cancellationToken);
                    break;
                case "None":
                    await IntentNone(stepContext,luisResult,cancellationToken);
                    break;
                case "VerOpciones":
                    await IntentVerOpciones(stepContext, luisResult, cancellationToken);
                    break;
            }
            return await stepContext.NextAsync(cancellationToken);
        }




        #region IntentLuis
        private async Task<DialogTurnResult> IntentCalificar(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(QualificatioDialog),cancellationToken:cancellationToken);
        }

        private async Task IntentVerOpciones(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Aqui tengo mis opciones, espero te ayuden", cancellationToken: cancellationToken);
            await MainOptionsCard.ToShow(stepContext, cancellationToken);
        }

        private async Task IntentNone(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("No entiendo lo que me dices,por favor repitelo de una manera más precisa y detallada", cancellationToken: cancellationToken);
        }

        private async Task IntentMesaAyuda(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            string person =$"Te puedes contactar con las siguientes personas para que te ayuden con tu solicitud: 😎{Environment.NewLine}" +
                $"Jhoan Tapia\t(jhoantapia@kpmg.com){Environment.NewLine}" +
                $"Jhon Guzman\t(jhonguzman@kpmg.com){Environment.NewLine}" +
                $"Jhansen Carodozo\t(jhansencardozo@kpmg.com){Environment.NewLine}" +
                $"Camilo Suarez\t(casuarez@kpmg.com)";

            await stepContext.Context.SendActivityAsync(person,cancellationToken: cancellationToken);
            await Task.Delay(1000);
            await stepContext.Context.SendActivityAsync("¿En qué más te puedo ayudar?");
        }

        

        private async Task IntentAgradecer(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("No te preocupes, entre compañeros siempre nos ayudamos 😉", cancellationToken: cancellationToken);
        }

        private async Task IntentDespedirse(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Que te vaya muy bien, espero verte pronto 👐", cancellationToken: cancellationToken);
        }

        private async Task IntentSaludos(WaterfallStepContext stepContext, RecognizerResult luisResult, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync("Hola,soy Piper ¿En que te puedo ayudar? 😄", cancellationToken:cancellationToken);
        }
        #endregion
        private Task<DialogTurnResult> FinalProccess(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return stepContext.EndDialogAsync(cancellationToken:cancellationToken);
        }

       
    }
}

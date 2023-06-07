using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PiperBotTI.Common.Cards
{
    public class MainOptionsCard
    {
        public static async Task ToShow(DialogContext stepContext, CancellationToken cancellationToken)
        {
            await stepContext.Context.SendActivityAsync(activity:CreateCarousel(), cancellationToken);
        }

        private static Activity CreateCarousel()
        {
            var cardMesaAyuda = new HeroCard
            {
                Title = "Asesores",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://piperbotstorage.blob.core.windows.net/images/menu_ayuda.jpg") },
                Buttons = new List<CardAction>()
                {
                    new CardAction() { Title = "Centro de contacto", Value = "Centro de contacto", Type = ActionTypes.ImBack },
                    
                }
            };

            var cardSiguenosRedes = new HeroCard
            {
                Title = "Siguenos en redes",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://piperbotstorage.blob.core.windows.net/images/menu_redes.jpg") },
                Buttons = new List<CardAction>()
                {
                    new CardAction() { Title = "Instagram", Value = "https://www.instagram.com/kpmgcolombia/", Type = ActionTypes.OpenUrl },
                    new CardAction(){Title =   "Youtube", Value = "https://www.youtube.com/results?search_query=kpmg+colombia", Type = ActionTypes.OpenUrl},
                    new CardAction(){Title =   "Facebook", Value = "https://www.facebook.com/kpmgcolombia/", Type = ActionTypes.OpenUrl},
                }
            };

            var cardCalificacion = new HeroCard
            {
                Title = "Calificacion",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://piperbotstorage.blob.core.windows.net/images/menu_calificacion.jpg") },
                Buttons = new List<CardAction>()
                {
                    new CardAction() { Title = "Calificar Bot", Value = "Calificar Bot", Type = ActionTypes.ImBack },

                }
            };


            var optionsAttachments = new List<Attachment>()
            {
                cardMesaAyuda.ToAttachment(),
                cardSiguenosRedes.ToAttachment(),
                cardCalificacion.ToAttachment()
            };

            var reply = MessageFactory.Attachment(optionsAttachments);
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            return reply as Activity;



        }

    }

}   

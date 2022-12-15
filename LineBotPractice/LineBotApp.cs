using Line.Messaging;
using Line.Messaging.Webhooks;

public class LineBotApp : WebhookApplication
{
    private readonly LineMessagingClient _messagingClient;
    public LineBotApp(LineMessagingClient lineMessagingClient)
    {
        _messagingClient = lineMessagingClient;
    }

    /// <summary>
    /// OnMessageAsync: 接收使用者訊息。
    /// </summary>
    /// <param name="ev"></param>
    /// <returns></returns>
    protected override async Task OnMessageAsync(MessageEvent ev)
    {
        //var result = null as List<ISendMessage>;

        //switch (ev.Message)
        //{
        //    //文字訊息
        //    case TextEventMessage textMessage:
        //        {
        //            //頻道Id
        //            var channelId = ev.Source.Id;
        //            //使用者Id
        //            var userId = ev.Source.UserId;

        //            //回傳 hellow
        //            result = new List<ISendMessage>
        //            {
        //                new TextMessage("hello world~~")
        //            };
        //        }
        //        break;
        //}

        //if (result != null)
        //    await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);  //ReplyMessageAsync: 傳訊息給使用者

        var buttonComponent = new ButtonComponent
        {
            Style = ButtonStyle.Primary,
            Action = new UriTemplateAction("LIFF", "line://app/1657738058-89BMdkJ6")
        };

        var flexMessage = new FlexMessage("LIFF")
        {
            Contents = new BubbleContainer
            {
                Body = new BoxComponent
                {
                    Layout = BoxLayout.Vertical,
                    Contents = new List<IFlexComponent>
                    {
                     buttonComponent
                    }
                }
            }
        };

        await _messagingClient.ReplyMessageAsync(ev.ReplyToken,new List<ISendMessage> { flexMessage });
    }
}
using Line.Messaging;
using Line.Messaging.Webhooks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

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
        //頻道Id
        var channelId = ev.Source.Id;
        //使用者Id
        var userId = ev.Source.UserId;

        switch (ev.Message)
        {
            //文字訊息
            case TextEventMessage msg:
                await HandleTextAsync(ev.ReplyToken, ((TextEventMessage)ev.Message).Text, ev.Source.UserId);
                break;
            case StickerEventMessage sticker:
                await HandleStickerAsync(ev.ReplyToken);
                break;
        }
    }


    private async Task HandleTextAsync(string replyToken, string userMessage, string userId)
    {

        if (userMessage == "hi")
        {
            List<ISendMessage> result;

            //回傳 hello
            result = new List<ISendMessage> { new TextMessage("hello~你好!我是ibot小幫手") };

            //ReplyMessageAsync: 傳訊息給使用者
            await _messagingClient.ReplyMessageAsync(replyToken, result);
        }
        else
        {
            var buttonComponent = new ButtonComponent
            {
                Style = ButtonStyle.Primary,
                Action = new UriTemplateAction("LIFF", "line://app/1657738058-89BMdkJ6")
            };

            var flexMessage = new FlexMessage("LIFF")
            {
                Contents = new BubbleContainer
                {
                    Header = new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                    },
                    Hero = new ImageComponent
                    {
                        Url = "https://upload.wikimedia.org/wikipedia/zh/3/39/Lighter_and_Princess.png"
                    },
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

            //ReplyMessageAsync: 傳訊息給使用者
            await _messagingClient.ReplyMessageAsync(replyToken, new List<ISendMessage> { flexMessage }); 
        }

    }


    /// <summary>
    /// Replies random sticker
    /// Sticker ID of bssic stickers (packge ID =1)
    /// see https://devdocs.line.me/files/sticker_list.pdf
    /// </summary>
    private async Task HandleStickerAsync(string replyToken)
    {
        var stickerids = Enumerable.Range(1, 17)
            .Concat(Enumerable.Range(21, 1))
            .Concat(Enumerable.Range(100, 139 - 100 + 1))
            .Concat(Enumerable.Range(401, 430 - 400 + 1)).ToArray();

        var rand = new Random(Guid.NewGuid().GetHashCode());
        var stickerId = stickerids[rand.Next(stickerids.Length - 1)].ToString();
        await _messagingClient.ReplyMessageAsync(replyToken, new[] {
                        new StickerMessage("1", stickerId)
                    });
    }
}
namespace CyberSecurityChatbot.Services
{
    public class Chatbot
    {
        public string GetResponse(string input, string userName)
        {
            input = input.ToLower();

            if (input.Contains("how are you"))
                return $"I'm doing great, {userName}! Ready to help you stay safe online.";

            if (input.Contains("purpose"))
                return "My purpose is to educate you about cybersecurity and keep you safe online.";

            if (input.Contains("ask"))
                return "You can ask me about passwords, phishing, scams, and safe browsing.";

            if (input.Contains("password"))
                return "Use strong passwords with a mix of uppercase, lowercase, numbers, and symbols.";

            if (input.Contains("phishing"))
                return "Phishing is when attackers trick you into giving personal information through fake emails or links.";

            if (input.Contains("scam"))
                return "Scammers try to trick you into sending money or information. Always verify sources.";

            if (input.Contains("safe browsing"))
                return "Only visit secure websites and avoid clicking suspicious links.";

            return "I didn’t quite understand that. Could you rephrase?";
        }
    }
}
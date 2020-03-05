using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalX.ChatBots.Core.Messages;
using PockyBot.NET.Constants;
using PockyBot.NET.Persistence.Repositories;

namespace PockyBot.NET.Services.Triggers
{
    internal class StringConfig : ITrigger
    {
        private readonly IConfigRepository _configRepository;
        public string Command => Commands.StringConfig;
        public bool DirectMessageAllowed => false;
        public bool CanHaveArgs => true;
        public string[] Permissions => new[] {Roles.Admin, Roles.Config};

        public StringConfig(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<Message> Respond(Message message)
        {
            var command = string.Join("", message.MessageParts.Skip(1).Select(x => x.Text)).Trim().Remove(0, 4).Trim();
            var commandWords = command.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            if (commandWords.Count() < 2)
            {
                return new Message
                {
                    Text = $"Please specify a command. Possible values are {string.Join(", ", ConfigActions.All())}."
                };
            }

            string newMessage;
            switch (commandWords[1])
            {
                case ConfigActions.Get:
                    newMessage = GetStringConfig();
                    break;
                case ConfigActions.Add:
                    newMessage = await AddStringConfig(commandWords).ConfigureAwait(false);
                    break;
                case ConfigActions.Delete:
                    newMessage = await DeleteStringConfig(commandWords).ConfigureAwait(false);
                    break;
                default:
                    newMessage = $"Invalid string config command. Possible values are {string.Join(", ", ConfigActions.All())}.";
                    break;
            }

            return new Message
            {
                Text = newMessage
            };
        }

        private string GetStringConfig()
        {
            var stringConfig = _configRepository.GetAllStringConfig();
            var groupedConfig = stringConfig.GroupBy(x => x.Name);

            var stringBuilder = new StringBuilder("Here is the current config (**name:** value):\n");
            foreach (var grouping in groupedConfig)
            {
                if (grouping.Count() == 1)
                {
                    stringBuilder.Append($"* **{grouping.Key}:** {grouping.First().Value}\n");
                }
                else
                {
                    stringBuilder.Append($"* **{grouping.Key}:**\n");
                    foreach (var config in grouping)
                    {
                        stringBuilder.Append($"    * {config.Value}\n");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        private async Task<string> AddStringConfig(List<string> commandWords)
        {
            if (commandWords.Count != 4) {
                return "You must specify a config name and value to add.";
            }

            if (_configRepository.GetStringConfig(commandWords[2]).Contains(commandWords[3], StringComparer.OrdinalIgnoreCase))
            {
                return $"String config name:value pair {commandWords[2]}:{commandWords[3]} already exists.";
            }

            await _configRepository.AddStringConfig(commandWords[2].ToLower(CultureInfo.InvariantCulture), commandWords[3]);
            return $"Config has been updated: {commandWords[2]}:{commandWords[3]} has been added.";
        }

        private async Task<string> DeleteStringConfig(List<string> commandWords)
        {
            if (commandWords.Count != 4) {
                return "You must specify a config name and value to be deleted.";
            }

            if (!_configRepository.GetStringConfig(commandWords[2]).Contains(commandWords[3], StringComparer.OrdinalIgnoreCase)) {
                return $"String config name:value pair {commandWords[2]}:{commandWords[3]} does not exist.";
            }

            await _configRepository.DeleteStringConfig(commandWords[2].ToLower(CultureInfo.InvariantCulture), commandWords[3]);
            return $"Config has been updated: {commandWords[2]}:{commandWords[3]} has been deleted.";
        }
    }
}

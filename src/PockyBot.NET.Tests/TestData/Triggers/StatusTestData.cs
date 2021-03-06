using System;
using System.Collections.Generic;
using GlobalX.ChatBots.Core.Messages;
using GlobalX.ChatBots.Core.People;
using PockyBot.NET.Constants;
using PockyBot.NET.Persistence.Models;

namespace PockyBot.NET.Tests.TestData.Triggers
{
    public static class StatusTestData
    {
        public static IEnumerable<object[]> RespondTestData()
        {
            // No pegs given
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>(),
                    Roles = new List<UserRole>()
                },
                5, // limit
                new List<Tuple<string, bool>>(),
                new Message
                {
                    Text = "You have 5 pegs left to give.\n\nYou have not given any pegs so far.",
                    RoomId = "testUserId"
                }
            };

            // user does not exist
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                null,
                5, // limit
                new List<Tuple<string, bool>>(),
                new Message
                {
                    Text = "You have 5 pegs left to give.\n\nYou have not given any pegs so far.",
                    RoomId = "testUserId"
                }
            };

            // Some pegs given, no penalties
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "This is a comment", Receiver = new PockyUser { Username = "Test User 2" } },
                        new Peg{ Comment = "This is another comment", Receiver = new PockyUser { Username = "Test User 3" } }
                    },
                    Roles = new List<UserRole>()
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("This is a comment", true),
                    new Tuple<string, bool>("This is another comment", true)
                },
                new Message
                {
                    Text = "You have 3 pegs left to give.\n\nHere are the pegs you've given so far:\n* **Test User 2** — \"_This is a comment_\"\n* **Test User 3** — \"_This is another comment_\"",
                    RoomId = "testUserId"
                }
            };

            // Some penalties, no pegs
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "Shame shame", Receiver = new PockyUser { Username = "Test User 4" } },
                        new Peg{ Comment = "More shame", Receiver = new PockyUser { Username = "Test User 5" } }
                    },
                    Roles = new List<UserRole>()
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("Shame shame", false),
                    new Tuple<string, bool>("More shame", false)
                },
                new Message
                {
                    Text = "You have 5 pegs left to give.\n\nHere are the penalties you have received:\n* **Test User 4** — \"_Shame shame_\"\n* **Test User 5** — \"_More shame_\"",
                    RoomId = "testUserId"
                }
            };

            // Some pegs and penalties given
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "This is a comment", Receiver = new PockyUser { Username = "Test User 2" } },
                        new Peg{ Comment = "This is another comment", Receiver = new PockyUser { Username = "Test User 3" } },
                        new Peg{ Comment = "Shame shame", Receiver = new PockyUser { Username = "Test User 4" } },
                        new Peg{ Comment = "More shame", Receiver = new PockyUser { Username = "Test User 5" } }
                    },
                    Roles = new List<UserRole>()
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("This is a comment", true),
                    new Tuple<string, bool>("This is another comment", true),
                    new Tuple<string, bool>("Shame shame", false),
                    new Tuple<string, bool>("More shame", false)
                },
                new Message
                {
                    Text = "You have 3 pegs left to give.\n\nHere are the pegs you've given so far:\n* **Test User 2** — \"_This is a comment_\"\n* **Test User 3** — \"_This is another comment_\"\n\nHere are the penalties you have received:\n* **Test User 4** — \"_Shame shame_\"\n* **Test User 5** — \"_More shame_\"",
                    RoomId = "testUserId"
                }
            };

            // Unmetered user
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>(),
                    Roles = new List<UserRole>
                    {
                        new UserRole { Role = Role.Unmetered }
                    }
                },
                5, // limit
                new List<Tuple<string, bool>>(),
                new Message
                {
                    Text = "You have unlimited pegs left to give.\n\nYou have not given any pegs so far.",
                    RoomId = "testUserId"
                }
            };

            // Unmetered user, some pegs given
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "This is a comment", Receiver = new PockyUser { Username = "Test User 2" } },
                        new Peg{ Comment = "This is another comment", Receiver = new PockyUser { Username = "Test User 3" } }
                    },
                    Roles = new List<UserRole>
                    {
                        new UserRole { Role = Role.Unmetered }
                    }
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("This is a comment", true),
                    new Tuple<string, bool>("This is another comment", true)
                },
                new Message
                {
                    Text = "You have unlimited pegs left to give.\n\nHere are the pegs you've given so far:\n* **Test User 2** — \"_This is a comment_\"\n* **Test User 3** — \"_This is another comment_\"",
                    RoomId = "testUserId"
                }
            };

            // Unmetered user, Some penalties, no pegs
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "Shame shame", Receiver = new PockyUser { Username = "Test User 4" } },
                        new Peg{ Comment = "More shame", Receiver = new PockyUser { Username = "Test User 5" } }
                    },
                    Roles = new List<UserRole>
                    {
                        new UserRole{ Role = Role.Unmetered }
                    }
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("Shame shame", false),
                    new Tuple<string, bool>("More shame", false)
                },
                new Message
                {
                    Text = "You have unlimited pegs left to give.\n\nHere are the penalties you have received:\n* **Test User 4** — \"_Shame shame_\"\n* **Test User 5** — \"_More shame_\"",
                    RoomId = "testUserId"
                }
            };

            // Unmetered user, some pegs and penalties given
            yield return new object[]
            {
                new Message
                {
                    Text = "TestBot status",
                    MessageParts = new[]
                    {
                        new MessagePart
                        {
                            Text = "TestBot",
                            MessageType = MessageType.PersonMention,
                            UserId = "testBotId"
                        },
                        new MessagePart
                        {
                            Text = " status",
                            MessageType = MessageType.Text
                        }
                    },
                    Sender = new Person
                    {
                        UserId = "testUserId",
                        Username = "Test User",
                        Type = PersonType.Person
                    }
                },
                new PockyUser // sender
                {
                    UserId = "testUserId",
                    Username = "Test User",
                    PegsGiven = new List<Peg>
                    {
                        new Peg{ Comment = "This is a comment", Receiver = new PockyUser { Username = "Test User 2" } },
                        new Peg{ Comment = "This is another comment", Receiver = new PockyUser { Username = "Test User 3" } },
                        new Peg{ Comment = "Shame shame", Receiver = new PockyUser { Username = "Test User 4" } },
                        new Peg{ Comment = "More shame", Receiver = new PockyUser { Username = "Test User 5" } }
                    },
                    Roles = new List<UserRole>
                    {
                        new UserRole{ Role = Role.Unmetered }
                    }
                },
                5, // limit
                new List<Tuple<string, bool>>
                {
                    new Tuple<string, bool>("This is a comment", true),
                    new Tuple<string, bool>("This is another comment", true),
                    new Tuple<string, bool>("Shame shame", false),
                    new Tuple<string, bool>("More shame", false)
                },
                new Message
                {
                    Text = "You have unlimited pegs left to give.\n\nHere are the pegs you've given so far:\n* **Test User 2** — \"_This is a comment_\"\n* **Test User 3** — \"_This is another comment_\"\n\nHere are the penalties you have received:\n* **Test User 4** — \"_Shame shame_\"\n* **Test User 5** — \"_More shame_\"",
                    RoomId = "testUserId"
                }
            };
        }
    }
}

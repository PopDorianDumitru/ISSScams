using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject_Regenerated.ScamBots.Service
{
    internal interface IScamMessageGenerator
    {
        /// <summary>
        /// Generates a randomly generated scam message.
        /// </summary>
        /// <returns>A string containing a message template and an attached link to a malicious website.</returns>
        string GenerateScamMessage();

        /// <summary>
        /// Generates a list of randomly generated scam messages.
        /// </summary>
        /// <param name="count">The number of scam messages to generate.</param>
        /// <returns>A list of strings, each containing a message template and an attached link to a malicious website.</returns>
        List<string> GenerateScamMessages(int count);
    }
}

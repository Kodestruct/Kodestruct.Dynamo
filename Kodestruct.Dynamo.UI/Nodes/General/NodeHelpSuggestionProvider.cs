using KodestructDynamoUI.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControls;
using System.Collections;

namespace KodestructDynamoUI.Nodes.General
{
    public class NodeHelpSuggestionProvider: ISuggestionProvider
    {
        public NodeHelpSuggestionProvider()
        {
            provider = new NodeListProvider();
        }

        NodeListProvider provider;

        public IEnumerable GetSuggestions(string filter)
        {
            return provider.NodeItems.Select(i => i.FullName);
        }

    }
}

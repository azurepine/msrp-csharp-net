//
// Copyright © Massxess BV This file is part of MSRP-CSharp.net Stack.
// 
// MSRP-CSharp.net Stack is free software: you can redistribute it and/or modify it
// under the terms of the GNU Lesser General Public License as published by the
// Free Software Foundation, version 3 or later.
// 
// MSRP-CSharp.net Stack is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License
// for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with MSRP-CSharp.net Stack. If not, see <http://www.gnu.org/licenses/>.
//

using MSRP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSRP
{
    /// <summary>
    /// An outgoing MSRP message containing an IM message composition indication in XML.
    /// </summary>
    public class OutgoingStatusMessage : OutgoingMessage, IStatusMessage
    {
        private const string ISCOMPOSING =
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
			"<isComposing>" +
			"	<state>{0}</state>" +
			"	<contenttype>{1}</contenttype>" +
			"	{2}" +
			"</isComposing>";
        private const string REFRESH_PARAM = "<refresh>{0}</refresh>";

        private ImState _state = ImState.idle;
        private string _composeContentType = string.Empty;
        private DateTime _lastActive = DateTime.MinValue;
        private int _refresh = 0;

        public ImState State { get { return _state; } }
        public string ComposeContentType { get { return _composeContentType; } }
        public DateTime LastActive { get { return _lastActive; } }
        public int Refresh { get { return _refresh; } }

        public OutgoingStatusMessage(Session session, ImState state, string contentType, int refresh)
            : base(Message.IMCOMPOSE_TYPE, string.Format(ISCOMPOSING, state.ToString(), contentType, IncludeRefresh(refresh)).Encode(Encoding.UTF8))
        {
            _state = state;
            _composeContentType = contentType;
            _refresh = refresh;
        }

        static private string IncludeRefresh(int refresh)
        {
            return refresh > 0 ? string.Format(REFRESH_PARAM, refresh) : string.Empty;
        }
    }
}

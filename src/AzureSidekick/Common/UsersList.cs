// Decompiled with JetBrains decompiler
// Type: Attrice.TeamFoundation.Common.UsersList
// Assembly: Attrice.TeamFoundation.Common.12, Version=4.5.1.0, Culture=neutral, PublicKeyToken=35acb2d39e045fbd
// MVID: A152C2C1-BE36-402B-9F4C-D577D02612BB
// Assembly location: C:\Program Files (x86)\Attrice Corporation\Team Foundation Sidekicks 2015\Attrice.TeamFoundation.Common.12.dll

using System;
using System.Data;
using System.Threading;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.VisualStudio.Services.Common;

namespace Attrice.TeamFoundation.Common
{
    public class UsersList
    {
        private DataTable _dtUsers;
        private readonly EventWaitHandle _initialized;
        private int _numberOfUsers;
        private readonly IIdentityManagementService _identityManagement;

        public DataTable UsersTable
        {
            get
            {
                lock (_dtUsers)
                    return _dtUsers.Copy();
            }
        }

        public UsersList(IIdentityManagementService identityManagement, string defaultIdentity)
        {
            _dtUsers = ListTable.CreateListTable();

            var row = _dtUsers.NewRow();
            row[ListTable.ColumnID] = defaultIdentity;
            row[ListTable.ColumnValue] = defaultIdentity;
            _dtUsers.Rows.Add(row);

            _identityManagement = identityManagement;
            _initialized = new EventWaitHandle(true, EventResetMode.ManualReset);
            _numberOfUsers = 0;
        }

        public int NumberOfUsers => _numberOfUsers;

        public EventWaitHandle Initialized => _initialized;
        public void RetrieveUsers()
        {
            _initialized.Reset();
            _numberOfUsers = 0;
            var dtUsers = _dtUsers.Clone();
            var foundationIdentity = _identityManagement.ReadIdentity(GroupWellKnownDescriptors.EveryoneGroup, (MembershipQuery)2, 0);
            if (foundationIdentity.Members.Length > 100)
            {
                for (var sourceIndex = 0; sourceIndex < foundationIdentity.Members.Length; sourceIndex += 100)
                {
                    var length = 100;
                    if (sourceIndex + 100 > foundationIdentity.Members.Length)
                        length = foundationIdentity.Members.Length % 100;
                    var destinationArray = new IdentityDescriptor[length];
                    Array.Copy(foundationIdentity.Members, sourceIndex, destinationArray, 0, length);
                    var validUsers = _identityManagement.ReadIdentities(destinationArray, (MembershipQuery)1, 0);
                    AddUsersToTable(dtUsers, validUsers);
                }
            }
            else
            {
                var validUsers = _identityManagement.ReadIdentities(foundationIdentity.Members, (MembershipQuery)1, 0);
                AddUsersToTable(dtUsers, validUsers);
            }
            lock (_dtUsers)
                _dtUsers = dtUsers;
            _initialized.Set();
        }

        private void AddUsersToTable(DataTable dtUsers, TeamFoundationIdentity[] validUsers)
        {
            foreach (var validUser in validUsers)
            {
                if (validUser == null ||
                    (validUser.Descriptor.IdentityType != IdentityConstants.WindowsType &&
                     validUser.Descriptor.IdentityType != IdentityConstants.ClaimsType) ||
                    validUser.IsContainer) continue;
                var row = dtUsers.NewRow();
                row[ListTable.ColumnID] = validUser.GetAttribute("Domain", "") + "\\" + validUser.GetAttribute("Account", "");
                row[ListTable.ColumnValue] = validUser.DisplayName;
                dtUsers.Rows.Add(row);
                ++_numberOfUsers;
            }
        }

    }
}
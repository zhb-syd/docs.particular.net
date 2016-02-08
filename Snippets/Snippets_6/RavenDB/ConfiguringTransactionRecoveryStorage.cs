﻿namespace Snippets5.RavenDB
{
    using NServiceBus;
    using Raven.Client.Document;
    using Raven.Client.Document.DTC;

    class ConfiguringTransactionRecoveryStorage
    {
        public void Foo()
        {
            // Do not include ConfiguringTransactionRecoveryStorage region. Core V6 must use 
            // Raven V4 which will not include the issue that necessitates this workaround.
        }
    }
}
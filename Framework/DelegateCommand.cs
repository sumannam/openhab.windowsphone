﻿/* openHAB, the open Home Automation Bus.
 * Copyright (C) 2010-${year}, openHAB.org <admin@openhab.org>
 * 
 * See the contributors.txt file in the distribution for a
 * full listing of individual contributors.
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 3 of the
 * License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, see <http://www.gnu.org/licenses>.
 * 
 * Additional permission under GNU GPL version 3 section 7
 * 
 * If you modify this Program, or any covered work, by linking or 
 * combining it with Eclipse (or a modified version of that library),
 * containing parts covered by the terms of the Eclipse Public License
 * (EPL), the licensors of this Program grant you additional permission
 * to convey the resulting work.
 */
 
using System;
using System.Windows.Input;

namespace Framework
{
    public class DelegateCommand : ICommand
    {
        public delegate bool CanExecuteHandler(object aParameter);
        public delegate void ExecuteHandler(object aParameter);

        public DelegateCommand()
        {
            // empty ctor for serialization
        }

        public DelegateCommand(ExecuteHandler aExecute)
            : this(aExecute, null, null)
        {
        }

        public DelegateCommand(ExecuteHandler aExecute, object aOverwriteParameter)
            : this(aExecute, null, aOverwriteParameter)
        {
        }

        public DelegateCommand(ExecuteHandler aExecute, CanExecuteHandler aCanExecute)
            : this(aExecute, aCanExecute, null)
        {
        }

        public DelegateCommand(ExecuteHandler aExecute, CanExecuteHandler aCanExecute, object aOverwriteParameter)
        {
            mExecute = aExecute;
            mCanExecute = aCanExecute;
            mOverwriteParameter = aOverwriteParameter;
        }

        private readonly object mOverwriteParameter;
        private readonly ExecuteHandler mExecute;
        private readonly CanExecuteHandler mCanExecute;

        #region ICommand Members

        public bool CanExecute(object aParameter)
        {
            return mCanExecute == null || mCanExecute(mOverwriteParameter ?? aParameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add {}
            remove {}
        }

        public void Execute(object aParameter)
        {
            mExecute(mOverwriteParameter ?? aParameter);
        }

        #endregion
    }
}

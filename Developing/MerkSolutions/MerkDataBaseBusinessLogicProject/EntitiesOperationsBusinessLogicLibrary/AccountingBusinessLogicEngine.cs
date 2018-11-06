using System;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary
{
	public class AccountingBusinessLogicEngine
	{
		public static CashBoxInOutTransaction CreateCashBoxInOutTransaction(object transactionDate,
			object cashBoxTransactionType_P_ID, object chartOfAccount_CU_ID, object generalChartOfAccountType_CU_ID,
			object transactionAmount, object paymentType_P_ID, object cashBox_CU_ID, object bank_CU_ID, object bankAccount_CU_ID,
			object currency_CU_ID, object currencyExchangeRate, object transcationSerial, object description)
		{
			if (transactionDate == null || cashBoxTransactionType_P_ID == null || chartOfAccount_CU_ID == null ||
			    generalChartOfAccountType_CU_ID == null || transactionAmount == null || paymentType_P_ID == null ||
			    currency_CU_ID == null)
				return null;

			CashBoxInOutTransaction cashBoxInOutTransaction = DBCommon.CreateNewDBEntity<CashBoxInOutTransaction>();
			cashBoxInOutTransaction.TranscationDate = Convert.ToDateTime(transactionDate);
			cashBoxInOutTransaction.CashBoxTransactionType_P_ID = Convert.ToInt32(cashBoxTransactionType_P_ID);
			cashBoxInOutTransaction.ChartOfAccount_CU_ID = Convert.ToInt32(chartOfAccount_CU_ID);
			cashBoxInOutTransaction.GeneralChartOfAccountType_CU_ID = Convert.ToInt32(generalChartOfAccountType_CU_ID);
			cashBoxInOutTransaction.TransactionAmount = Convert.ToDouble(transactionAmount);
			cashBoxInOutTransaction.PaymentType_P_ID = Convert.ToInt32(paymentType_P_ID);
			cashBoxInOutTransaction.Currency_CU_ID = Convert.ToInt32(currency_CU_ID);
			if (currencyExchangeRate != null)
				cashBoxInOutTransaction.CurrencyExchangeRate = Convert.ToDouble(currencyExchangeRate);
			if (transcationSerial != null)
				cashBoxInOutTransaction.TranscationSerial = transcationSerial.ToString();
			cashBoxInOutTransaction.IsCancelled = false;

			return cashBoxInOutTransaction;
		}

		public static AccountingJournalTransaction CreateAccountingJournalTransaction(object jounralSerial,
			object transactionDate, object transactionAmount, object financialTransactionType_P_ID, object description)
		{
			if (jounralSerial == null || transactionDate == null || transactionAmount == null ||
			    financialTransactionType_P_ID == null)
				return null;

			AccountingJournalTransaction accountingJournalTransaction =
				DBCommon.CreateNewDBEntity<AccountingJournalTransaction>();

			accountingJournalTransaction.JounralSerial = jounralSerial.ToString();
			accountingJournalTransaction.TransactionDate = Convert.ToDateTime(transactionDate);
			accountingJournalTransaction.TransactionAmount = Convert.ToDouble(transactionAmount);
			accountingJournalTransaction.FinancialTransactionType_P_ID = Convert.ToInt32(financialTransactionType_P_ID);
			if (description != null)
				accountingJournalTransaction.Description = description.ToString();

			return accountingJournalTransaction;
		}

		public static AccountingJournalEntryTransaction CreateAccountingJournalEntryTransaction(
			AccountingJournalTransaction accountingJournalTransaction, object amount, object serial, object chartOfAccount_CU_ID, object isDebit,
			object description)
		{
			if (accountingJournalTransaction == null || amount == null || serial == null || chartOfAccount_CU_ID == null || isDebit == null)
				return null;

			AccountingJournalEntryTransaction accountingJournalEntryTransaction =
				DBCommon.CreateNewDBEntity<AccountingJournalEntryTransaction>();
			accountingJournalEntryTransaction.Amount = Convert.ToDouble(amount);
			accountingJournalEntryTransaction.Serial = serial.ToString();
			accountingJournalEntryTransaction.AccountingJournalTransaction = accountingJournalTransaction;
			accountingJournalEntryTransaction.ChartOfAccount_CU_ID = Convert.ToInt32(chartOfAccount_CU_ID);
			accountingJournalEntryTransaction.IsDebit = Convert.ToBoolean(isDebit);
			if (description != null)
				accountingJournalEntryTransaction.Description = description.ToString();

			return accountingJournalEntryTransaction;
		}

		public static AccountingJournalEntryTransaction CreateAccountingJournalEntryTransaction(
			object accountingJournalTransaction_ID, object amount, object serial, object chartOfAccount_CU_ID, object isDebit,
			object description)
		{
			if (amount == null || serial == null || accountingJournalTransaction_ID == null
			    || chartOfAccount_CU_ID == null || isDebit == null)
				return null;

			AccountingJournalEntryTransaction accountingJournalEntryTransaction =
				DBCommon.CreateNewDBEntity<AccountingJournalEntryTransaction>();
			accountingJournalEntryTransaction.Amount = Convert.ToDouble(amount);
			accountingJournalEntryTransaction.Serial = serial.ToString();
			accountingJournalEntryTransaction.AccountingJournalTransaction_ID = Convert.ToInt32(accountingJournalTransaction_ID);
			accountingJournalEntryTransaction.ChartOfAccount_CU_ID = Convert.ToInt32(chartOfAccount_CU_ID);
			accountingJournalEntryTransaction.IsDebit = Convert.ToBoolean(isDebit);
			if (description != null)
				accountingJournalEntryTransaction.Description = description.ToString();

			return accountingJournalEntryTransaction;
		}

		public static List<AccountingJournalEntryTransaction> SortAccountingJournalEntries(
			List<AccountingJournalEntryTransaction> accountingJournalEntries)
		{
			List<AccountingJournalEntryTransaction> creditJournalEntries = new List<AccountingJournalEntryTransaction>();
			List<AccountingJournalEntryTransaction> debitJournalEntries = new List<AccountingJournalEntryTransaction>();

			foreach (AccountingJournalEntryTransaction entryTransaction in accountingJournalEntries)
			{
				if(entryTransaction.IsDebit)
					debitJournalEntries.Add(entryTransaction);
				else
					creditJournalEntries.Add(entryTransaction);
			}

			accountingJournalEntries.Clear();
			accountingJournalEntries.AddRange(debitJournalEntries);
			accountingJournalEntries.AddRange(creditJournalEntries);
			return accountingJournalEntries;
		}

		public static List<ChartOfAccount_cu> GetChartOfAccountOfPreviousCodeMargin(object chartOfAccountCodeMarginID, bool isDebit)
		{
			if (chartOfAccountCodeMarginID == null)
				return null;

			ChartOfAccountCodeMargin_p codeMargin =
				ChartOfAccountCodeMargin_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(chartOfAccountCodeMarginID)));
			if (codeMargin == null)
				return null;
			DB_ChartOfAccountCodeMargin chartOfAccountCodeMargin = (DB_ChartOfAccountCodeMargin)Convert.ToInt32(codeMargin.ID);
			return GetChartOfAccountOfPreviousCodeMargin(chartOfAccountCodeMargin, isDebit);
		}

		public static List<ChartOfAccount_cu> GetChartOfAccountOfPreviousCodeMargin(
			DB_ChartOfAccountCodeMargin chartOfAccountCodeMargin, bool isDebit)
		{
			switch (chartOfAccountCodeMargin)
			{
				case DB_ChartOfAccountCodeMargin.FirstMargin:
					return null;
				case DB_ChartOfAccountCodeMargin.SecondMargin:
					return
						ChartOfAccount_cu.ItemsList.FindAll(
							item =>
								Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID).Equals((int)DB_ChartOfAccountCodeMargin.FirstMargin) &&
								Convert.ToBoolean(item.IsDebit).Equals(Convert.ToBoolean(isDebit)))
								.OrderBy(item => item.Serial)
							.OrderBy(item => item.ParentChartOfAccount_CU_ID).ToList();

				case DB_ChartOfAccountCodeMargin.ThirdMargin:
					return
						ChartOfAccount_cu.ItemsList.FindAll(
							item =>
								Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID).Equals((int)DB_ChartOfAccountCodeMargin.SecondMargin) &&
								Convert.ToBoolean(item.IsDebit).Equals(Convert.ToBoolean(isDebit)))
							.OrderBy(item => item.Serial)
							.OrderBy(item => item.ParentChartOfAccount_CU_ID).ToList();

				case DB_ChartOfAccountCodeMargin.FourthMargin:
					return
						ChartOfAccount_cu.ItemsList.FindAll(
							item =>
								Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID).Equals((int)DB_ChartOfAccountCodeMargin.ThirdMargin) &&
								Convert.ToBoolean(item.IsDebit).Equals(Convert.ToBoolean(isDebit)))
							.OrderBy(item => item.Serial)
							.OrderBy(item => item.ParentChartOfAccount_CU_ID).ToList();

				case DB_ChartOfAccountCodeMargin.FifthMargin:
					return
						ChartOfAccount_cu.ItemsList.FindAll(
							item =>
								Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID).Equals((int)DB_ChartOfAccountCodeMargin.FourthMargin) &&
								Convert.ToBoolean(item.IsDebit).Equals(Convert.ToBoolean(isDebit)))
							.OrderBy(item => item.Serial)
							.OrderBy(item => item.ParentChartOfAccount_CU_ID).ToList();
			}

			return null;
		}

		public static ChartOfAccount_cu GetChartOfAccount(object chartOfAccountID)
		{
			if (chartOfAccountID == null)
				return null;
			return ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(chartOfAccountID)));
		}

		public static long GetChartOfAccountSerial(object chartOfAccountID, object serialToBeAddedAtLast)
		{
			if (chartOfAccountID == null)
				return 1;
			ChartOfAccount_cu chartOfAccount =
				ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(chartOfAccountID)));
			if (chartOfAccount == null)
				return 1;

			string serial = "";
			serial = serial + chartOfAccount.Serial;
			if (serialToBeAddedAtLast == null)
				return Convert.ToInt64(serial);

			return Convert.ToInt64(serial + serialToBeAddedAtLast);
		}

		public static ChartOfAccountCodeMargin_p GetChartOfAccountCodeMargin(object chartOfAccountCodeMarginID)
		{
			if (chartOfAccountCodeMarginID == null)
				return null;
			return
				ChartOfAccountCodeMargin_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(chartOfAccountCodeMarginID)));
		}

		public static DB_ChartOfAccountCodeMargin GetChartOfAccountCodeMargin(ChartOfAccountCodeMargin_p chartOfAccountCodeMargin)
		{
			if (chartOfAccountCodeMargin == null)
				return DB_ChartOfAccountCodeMargin.None;
			return (DB_ChartOfAccountCodeMargin)Convert.ToInt32(chartOfAccountCodeMargin.ID);
		}

		public static DB_ChartOfAccountCodeMargin GetChartOfAccountCodeMargin(ChartOfAccount_cu chartOfAccount)
		{
			if (chartOfAccount == null)
				return DB_ChartOfAccountCodeMargin.None;

			return (DB_ChartOfAccountCodeMargin)Convert.ToInt32(chartOfAccount.ChartOfAccountCodeMargin_P_ID);
		}

		public static int GetChartOfAccountCodeMarginNumberOfDigits(object chartOfAccountCodeMarginID)
		{
			ChartOfAccountCodeMargin_p chartOfAccountCode = GetChartOfAccountCodeMargin(chartOfAccountCodeMarginID);
			if (chartOfAccountCode == null)
				return 0;

			int numberOfDigits = 0;
			DB_ChartOfAccountCodeMargin chosenAccountCodeMargin = (DB_ChartOfAccountCodeMargin)Convert.ToInt32(chartOfAccountCode.ID);
			switch (chosenAccountCodeMargin)
			{
				case DB_ChartOfAccountCodeMargin.FirstMargin:
					numberOfDigits = numberOfDigits + chartOfAccountCode.NumberOfDigits;
					break;
				case DB_ChartOfAccountCodeMargin.SecondMargin:
					numberOfDigits = numberOfDigits + Convert.ToInt32(chartOfAccountCode.NumberOfDigits) +
									 GetChartOfAccountCodeMarginNumberOfDigits((int)DB_ChartOfAccountCodeMargin.FirstMargin);
					break;
				case DB_ChartOfAccountCodeMargin.ThirdMargin:
					numberOfDigits = numberOfDigits + Convert.ToInt32(chartOfAccountCode.NumberOfDigits) +
									 GetChartOfAccountCodeMarginNumberOfDigits((int)DB_ChartOfAccountCodeMargin.SecondMargin);
					break;
				case DB_ChartOfAccountCodeMargin.FourthMargin:
					numberOfDigits = numberOfDigits + Convert.ToInt32(chartOfAccountCode.NumberOfDigits) +
									 GetChartOfAccountCodeMarginNumberOfDigits((int)DB_ChartOfAccountCodeMargin.ThirdMargin);
					break;
				case DB_ChartOfAccountCodeMargin.FifthMargin:
					numberOfDigits = numberOfDigits + Convert.ToInt32(chartOfAccountCode.NumberOfDigits) +
									 GetChartOfAccountCodeMarginNumberOfDigits((int)DB_ChartOfAccountCodeMargin.FourthMargin);
					break;
			}

			return numberOfDigits;
		}

		public static bool IsDebit(object chartOfAccountID)
		{
			ChartOfAccount_cu chartOfAccount = GetChartOfAccount(chartOfAccountID);
			if (chartOfAccount == null)
				return false;
			return chartOfAccount.IsDebit;
		}

		public static string GetNextChartOfAccountSerial(DB_ChartOfAccountCodeMargin chartOfAccountCodeMargin,
			object parentChartofAccountID)
		{
			string nextSerial = "1";

			if (ChartOfAccount_cu.ItemsList.Count == 0)
				return nextSerial;

			ChartOfAccount_cu parentChartOfAccount = null;
			if (parentChartofAccountID != null)
				parentChartOfAccount =
					ChartOfAccount_cu.ItemsList.Find(item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(parentChartofAccountID)));

			long parentSerial = -1;
			if (parentChartOfAccount != null)
				parentSerial = parentChartOfAccount.Serial;

			List<ChartOfAccount_cu> chartOfAccountsList =
				ChartOfAccount_cu.ItemsList.FindAll(
					item => Convert.ToInt32(item.ChartOfAccountCodeMargin_P_ID).Equals((int)chartOfAccountCodeMargin));
			if (parentChartOfAccount != null)
				chartOfAccountsList =
					chartOfAccountsList.FindAll(
						item =>
							item.ParentChartOfAccount_CU_ID != null &&
							Convert.ToInt32(item.ParentChartOfAccount_CU_ID).Equals(Convert.ToInt32(parentChartofAccountID)));
			ChartOfAccountCodeMargin_p chartOfAccountCodeMarginP =
				ChartOfAccountCodeMargin_p.ItemsList.Find(
					item => Convert.ToInt32(item.ID).Equals(Convert.ToInt32(chartOfAccountCodeMargin)));
			long allowedNumerOfDigits = GetChartOfAccountCodeMarginNumberOfDigits(Convert.ToInt32(chartOfAccountCodeMargin));
			long previousAllowedNumberOfDigits = 0;
			long chartOfAccountCodeMarginNumberOfDigits = 0;
			string numberOfZeros = "";
			ChartOfAccount_cu lastChartOfAccount = null;
			if (chartOfAccountsList.Count > 0)
				lastChartOfAccount = chartOfAccountsList.OrderByDescending(item => item.Serial).ToList().First();
			char[] arry = null;
			int index = 0;
			switch (chartOfAccountCodeMargin)
			{
				#region FirstMargin
				case DB_ChartOfAccountCodeMargin.FirstMargin:
					if (chartOfAccountsList.Count == 0)
						return nextSerial;
					if (lastChartOfAccount == null)
						return nextSerial;
					nextSerial = Convert.ToString(lastChartOfAccount.Serial + 1);
					if (nextSerial.Length > allowedNumerOfDigits)
						return String.Empty;
					break;
				#endregion

				#region SecondMargin
				case DB_ChartOfAccountCodeMargin.SecondMargin:
					if (chartOfAccountsList.Count == 0)
						return "0" + nextSerial;
					if (lastChartOfAccount == null)
						return "0" + nextSerial;
					nextSerial = Convert.ToString(lastChartOfAccount.Serial + 1);
					previousAllowedNumberOfDigits =
						GetChartOfAccountCodeMarginNumberOfDigits(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FirstMargin));
					arry = nextSerial.ToCharArray();
					if (arry.Length == 0)
						return string.Empty;
					index = 0;
					nextSerial = "";
					foreach (char character in arry)
					{
						index++;
						if (index > previousAllowedNumberOfDigits)
							nextSerial = nextSerial + character;
					}
					if (nextSerial.Length > allowedNumerOfDigits)
						return String.Empty;
					break;
				#endregion

				#region ThirdMargin
				case DB_ChartOfAccountCodeMargin.ThirdMargin:
					if (chartOfAccountsList.Count == 0)
						return "0" + nextSerial;
					if (lastChartOfAccount == null)
						return "0" + nextSerial;
					nextSerial = Convert.ToString(lastChartOfAccount.Serial + 1);
					previousAllowedNumberOfDigits =
						GetChartOfAccountCodeMarginNumberOfDigits(Convert.ToInt32(DB_ChartOfAccountCodeMargin.SecondMargin));
					arry = nextSerial.ToCharArray();
					if (arry.Length == 0)
						return string.Empty;
					index = 0;
					nextSerial = "";
					foreach (char character in arry)
					{
						index++;
						if (index > previousAllowedNumberOfDigits)
							nextSerial = nextSerial + character;
					}
					if (nextSerial.Length > allowedNumerOfDigits)
						return String.Empty;
					break;
				#endregion

				#region FourthMargin
				case DB_ChartOfAccountCodeMargin.FourthMargin:
					if (chartOfAccountsList.Count == 0)
						return "0" + nextSerial;
					if (lastChartOfAccount == null)
						return "0" + nextSerial;
					nextSerial = Convert.ToString(lastChartOfAccount.Serial + 1);
					previousAllowedNumberOfDigits =
						GetChartOfAccountCodeMarginNumberOfDigits(Convert.ToInt32(DB_ChartOfAccountCodeMargin.ThirdMargin));
					arry = nextSerial.ToCharArray();
					if (arry.Length == 0)
						return string.Empty;
					index = 0;
					nextSerial = "";
					foreach (char character in arry)
					{
						index++;
						if (index > previousAllowedNumberOfDigits)
							nextSerial = nextSerial + character;
					}
					if (nextSerial.Length > allowedNumerOfDigits)
						return String.Empty;
					break;
				#endregion

				#region FifthMargin
				case DB_ChartOfAccountCodeMargin.FifthMargin:
					if (chartOfAccountsList.Count == 0)
						return "00" + nextSerial;
					if (lastChartOfAccount == null)
						return "00" + nextSerial;

					previousAllowedNumberOfDigits =
						GetChartOfAccountCodeMarginNumberOfDigits(Convert.ToInt32(DB_ChartOfAccountCodeMargin.FourthMargin));
					allowedNumerOfDigits = allowedNumerOfDigits - previousAllowedNumberOfDigits;

					for (int i = 0; i < allowedNumerOfDigits; i++)
						numberOfZeros = numberOfZeros + "0";

					nextSerial = Convert.ToString(chartOfAccountsList.Count + 1);
					nextSerial = Convert.ToInt64(nextSerial).ToString(numberOfZeros);
					break;
				#endregion
			}

			return nextSerial;
		}
	}
}

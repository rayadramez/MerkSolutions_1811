﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MerkDataBaseBusinessLogicProject.EntitiesOperationsBusinessLogicLibrary;
using MerkDataBaseBusinessLogicProject.MerkDataBaseBusinessLogic.MerkModelCreateor.DBCommon;

namespace MerkDataBaseBusinessLogicProject
{
	public partial class InPatientRoomBed_InPatientAdmissionPricingType_cu : DBCommon, IDBCommon
	{
		MerkFinanceEntities _context;
		private static List<InPatientRoomBed_InPatientAdmissionPricingType_cu> _items;
		public static List<InPatientRoomBed_InPatientAdmissionPricingType_cu> ItemsList = new List<InPatientRoomBed_InPatientAdmissionPricingType_cu>();

		#region ColumnNames

		public static String InPatientRoomBed_CU_ID_ColumnaName
		{
			get { return "InPatientRoomBed_CU_ID"; }
		}

		public static String InPatientAddmissionPricingType_P_ID_ColumnaName
		{
			get { return "InPatientAddmissionPricingType_P_ID"; }
		}

		public static String PricePerDay_ColumnaName
		{
			get { return "PricePerDay"; }
		}

		public static String MinimumAddmissionAmount_ColumnaName
		{
			get { return "MinimumAddmissionAmount"; }
		}

		public static String InsuranceCarrier_InsuranceLevel_CU_ID_ColumnaName
		{
			get { return "InsuranceCarrier_InsuranceLevel_CU_ID"; }
		}

		#endregion

		public override bool LoadFromDB
		{
			get { return true; }
		}

		public override DBCommonEntitiesType TableType
		{
			get { return DBCommonEntitiesType.CustomUserEntities; }
		}

		public override bool LoadItemsList()
		{
			ItemsList.Clear();
			ItemsList = DBContext_External.InPatientRoomBed_InPatientAdmissionPricingType_cu.Where(item => item.IsOnDuty).OrderBy(item => item.ID).ToList();
			return true;
		}

		#region Implementation of IDBCommon

		public override IList ReGenerateList()
		{
			LoadItemsList();

			return ItemsList;
		}

		public int TableIdentityID
		{
			get { return (int)DB_TableIdentity.InPatientRoomBed_InPatientAdmissionPricingType_cu; }
		}

		List<string> IDBCommon.ChildrenItemsList
		{
			get { return null; }
		}

		public string EntityName
		{
			get { return "InPatientRoomBed_InPatientAdmissionPricingType_cu"; }
		}

		public IDBCommon GetSpecificEntity(MerkFinanceEntities context, int id)
		{
			return context.InPatientRoomBed_InPatientAdmissionPricingType_cu.FirstOrDefault(item => item.ID.Equals(id));
		}

		#endregion
	}
}
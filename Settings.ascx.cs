using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System.Collections;
using System.Web.UI.WebControls;
using GIBS.Modules.FlexMLS_Featured.Components;
using GIBS.Modules.FlexMLS.Components;
using System.Collections.Generic;
using DotNetNuke.Entities.Tabs;


namespace GIBS.Modules.FlexMLS_Featured
{
    public partial class Settings : FlexMLS_FeaturedSettings
    { 

        /// <summary>
        /// handles the loading of the module setting for this
        /// control
        /// </summary>
        public override void LoadSettings()
        {
            try
            {
                
                if (Page.IsPostBack == false)
                {

                    GetTowns();
                    GetMyTabs();

            //        FlexMLS_FeaturedSettings settingsData = new FlexMLS_FeaturedSettings(this.TabModuleId);

                    //settingsData.ThumbImageAlign = rblThumbPlacement.SelectedValue.ToString();


                    if (ShowPaging != null)
                    {
                        if(ShowPaging.Length > 0)
                        cbxShowPaging.Checked = Convert.ToBoolean(ShowPaging);
                    }
                    if (ShowTotalRecords != null)
                    {
                        if (ShowTotalRecords.Length > 0)
                        cbxTotalRecords.Checked = Convert.ToBoolean(ShowTotalRecords);
                    }
                    if (ShowCriteria != null)
                    {
                        if (ShowCriteria.Length > 0)
                        cbxShowCriteria.Checked = Convert.ToBoolean(ShowCriteria);
                    }

                    if (NumberOfRecords != null)
                    {
                        txtNumberOfRecords.Text = NumberOfRecords;
                    }
                    if (PropertyType != null)
                    {
                        ddlPropertyType.SelectedValue = PropertyType;
                    }
                    if (Town != null)
                    {
                        ddl_Town.SelectedValue = Town.ToString();
                    }
                    if (Village != null)
                    {
                        ddl_Village.SelectedValue = Village;
                    }
                    if (Bedrooms != null)
                    {
                        ddlBedRooms.SelectedValue = Bedrooms;
                    }
                    if (Bathrooms != null)
                    {
                        ddlBathRooms.SelectedValue = Bathrooms;
                    }
                    if (PriceLow != null)
                    {
                        ddlPriceLow.SelectedValue = PriceLow;
                    }
                    if (PriceHigh != null)
                    {
                        ddlPriceHigh.SelectedValue = PriceHigh;
                    }
                    if (WaterFront != null)
                    {
                        if (WaterFront.Length > 0)
                        cbxWaterFront.Checked = Convert.ToBoolean(WaterFront);
                    }
                    if (Waterview != null)
                    {
                        if (Waterview.Length > 0)
                        cbxWaterView.Checked = Convert.ToBoolean(Waterview);
                    }
                    if (Complex != null)
                    {
                        ddlComplex.SelectedValue = Complex;
                    }
                    if (DOM != null)
                    {
                        ddlDOM.SelectedValue = DOM;
                    }
                    if (FlexMLSPage != null)
                    {
                        ddlViewListing.SelectedValue = FlexMLSPage;
                    }
                    if (MLSImagesUrl != null)
                    {
                        if (MLSImagesUrl.Length > 0)
                        txtMLSImagesUrl.Text = MLSImagesUrl;
                    }
                    if (ItemCssClass != null && ItemCssClass.Length > 0)
                    {
                        if (ItemCssClass.Length > 0)
                        txtItemCssClass.Text = ItemCssClass;
                    }
                    if (ListingOfficeMLSID != null)
                    {
                        ddlOfficeID.SelectedValue = ListingOfficeMLSID;
                    }



                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// handles updating the module settings for this control
        /// </summary>
        public override void UpdateSettings()
        {
            try
            {

                MLSImagesUrl = txtMLSImagesUrl.Text.ToString();
                ShowPaging = cbxShowPaging.Checked.ToString();
                ShowCriteria = cbxShowCriteria.Checked.ToString();
                ShowTotalRecords = cbxTotalRecords.Checked.ToString();
                NumberOfRecords = txtNumberOfRecords.Text.ToString();
                PropertyType = ddlPropertyType.SelectedValue.ToString();
                Town = ddl_Town.SelectedValue.ToString();
                Village = ddl_Village.SelectedValue.ToString();
                Bedrooms = ddlBedRooms.SelectedValue.ToString();
                Bathrooms = ddlBathRooms.SelectedValue.ToString();
                PriceLow = ddlPriceLow.SelectedValue.ToString();
                PriceHigh = ddlPriceHigh.SelectedValue.ToString();
                WaterFront = cbxWaterFront.Checked.ToString();
                Waterview = cbxWaterView.Checked.ToString();
                Complex = ddlComplex.SelectedValue.ToString();
                DOM = ddlDOM.SelectedValue.ToString();
                FlexMLSPage = ddlViewListing.SelectedValue.ToString();
                ItemCssClass = txtItemCssClass.Text.ToString();

                ListingOfficeMLSID = ddlOfficeID.SelectedValue.ToString();
                

          //      var modules = new ModuleController();

          //      modules.UpdateModuleSetting(ModuleId, "MLSImagesUrl", txtMLSImagesUrl.Text.ToString());

          //      modules.UpdateModuleSetting(ModuleId, "ShowPaging", cbxShowPaging.Checked.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "ShowCriteria", cbxShowCriteria.Checked.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "ShowTotalRecords", cbxTotalRecords.Checked.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "NumberOfRecords", txtNumberOfRecords.Text.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "PropertyType", ddlPropertyType.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Town", ddl_Town.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Village", ddl_Village.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Bedrooms", ddlBedRooms.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Bathrooms", ddlBathRooms.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "PriceLow", ddlPriceLow.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "PriceHigh", ddlPriceHigh.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "WaterFront", cbxWaterFront.Checked.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Waterview", cbxWaterView.Checked.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "Complex", ddlComplex.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "DOM", ddlDOM.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "FlexMLSPage", ddlViewListing.SelectedValue.ToString());
          //      modules.UpdateModuleSetting(ModuleId, "ItemCssClass", txtItemCssClass.Text.ToString());
          ////      modules.UpdateModuleSetting(ModuleId, "ListingOfficeMLSID", ddlOfficeID.SelectedValue.ToString());
                

            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        public void GetMyTabs()
        {

            try
            {

                ddlViewListing.DataSource = TabController.GetTabsBySortOrder(this.PortalId);
                //ddlViewListing.DataSource = TabController.GetPortalTabs(this.PortalId, this.TabId, true, false);
                ddlViewListing.DataBind();


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        public void GetTowns()
        {

            try
            {

                List<FlexMLSInfo> items;
                List<FlexMLSInfo> itemsOffices;

                FlexMLSController controller = new FlexMLSController();
                items = controller.FlexMLS_Lookup_Town();


                ddl_Town.DataSource = items;
                ddl_Town.DataTextField = "Town";
                ddl_Town.DataValueField = "Town";
                ddl_Town.DataBind();

                ddl_Town.Items.Insert(0, new ListItem("--Select--", ""));

                //ddlOfficeID
                itemsOffices = controller.FlexMLS_Get_Offices();
                ddlOfficeID.DataSource = itemsOffices;
                ddlOfficeID.DataTextField = "ListingOfficeName";
                ddlOfficeID.DataValueField = "OfficeID";
                ddlOfficeID.DataBind();

                ddlOfficeID.Items.Insert(0, new ListItem("--Select--", ""));


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        protected void ddl_Town_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<FlexMLSInfo> items;
            FlexMLSController controller = new FlexMLSController();

            items = controller.FlexMLS_Lookup_Village(ddl_Town.SelectedValue.ToString());

            ddl_Village.DataSource = items;
            ddl_Village.DataTextField = "Village";
            ddl_Village.DataValueField = "Village";
            ddl_Village.DataBind();

            ddl_Village.Items.Insert(0, new ListItem("--Optionally Select--", ""));


        }
    }
}
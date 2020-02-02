
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.Modules.FlexMLS_Featured.Components;
using GIBS.Modules.FlexMLS.Components;
using DotNetNuke.Common;
using System.Net;
using System.IO;
using System.Text;

namespace GIBS.Modules.FlexMLS_Featured
{
    public partial class ViewFlexMLS_Featured : PortalModuleBase, IActionable
    {

        public string _FlexMLSPage = "0";
        public string _numberOfRecords = "1";
   //     public string _maxThumbSize = "200";
   //     public string _imageAlign = "left";

        public int CurrentPage;

        public string _propertyType = "";
        public string _village = "";
        public string _town = "";
        public string _beds = "0";
        public string _baths = "0";
        public string _waterfront = "";
        public string _waterview = "";
        public string _pricelow = "0";
        public string _pricehigh = "50000000";
      //  static string _returnURL = "";
        public string _listingOfficeMLSID = "";
        public string _complex = "";
        public string _dom = "";
        public bool _searchMlsNumber = false;
        public bool _condoSearch = false;
        public string _MlsNumbers = "";

        public string _MLSImagesURL = "";
        public string _ItemCssClass = "col-md-6 col-sm-6 col-xs-6";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                if (Settings.Contains("ItemCssClass"))
                {
                    _ItemCssClass = Settings["ItemCssClass"].ToString();
                }
                LoadSettings();

               // lblDebug.Text = _ItemCssClass.ToString();
                if (!IsPostBack)
                {
                    hidPage.Value = "0";
                    CurrentPage = 0;

                }
                else
                {
                    CurrentPage = Int32.Parse(hidPage.Value.ToString());
                    //CheckQueryString();
                    //SearchMLS();
                }
                CheckQueryString();

                // && Settings["FlexMLSPage"].ToString().Length > 0

                if (_MLSImagesURL.Length > 1 && _FlexMLSPage.ToString() != "0")
                {
                    SearchMLS();
                }
                else {
                    lblDebug.Text = "Missing Settings. Please correct.";
                }

                
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        public void CheckQueryString()
        {

            try
            {

                if (Request.QueryString["Complex"] != null)
                {
                    _complex = Request.QueryString["Complex"].ToString();
                    _complex = _complex.ToString().Replace("_", " ").ToString().Replace("~", "/").ToString().Replace("^", "'").ToString();
                    _condoSearch = true;
                }

                if (Request.QueryString["Town"] != null)
                {
                    _town = Request.QueryString["Town"].ToString();
                }

                if (Request.QueryString["Village"] != null)
                {
                    _village = Request.QueryString["Village"].ToString();
                }

                if (Request.QueryString["Beds"] != null)
                {
                    _beds = Request.QueryString["Beds"].ToString();
                }

                if (Request.QueryString["Baths"] != null)
                {
                    _baths = Request.QueryString["Baths"].ToString();
                }

                if (Request.QueryString["WaterFront"] != null)
                {
                    _waterfront = Request.QueryString["WaterFront"].ToString();
                }

                if (Request.QueryString["WaterView"] != null)
                {
                    _waterview = Request.QueryString["WaterView"].ToString();
                }



                if (Request.QueryString["LOID"] != null)
                {
                    _listingOfficeMLSID = Request.QueryString["LOID"].ToString();
                }

                if (Request.QueryString["DOM"] != null)
                {
                    _dom = Request.QueryString["DOM"].ToString();
                }
                // listingOfficeMLSID



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        public void SearchMLS()
        {

            try
            {




                int _bedRooms = Convert.ToInt32(_beds.ToString());
                int _bathRooms = Convert.ToInt32(_baths.ToString());
                string _SearchWaterFront = "";
                string _SearchWaterView = "";
                int _priceLow = Convert.ToInt32(_pricelow.ToString());
                int _priceHigh = Convert.ToInt32(_pricehigh.ToString());

                if (_waterfront.ToString() == "True")
                {
                    _SearchWaterFront = "YES";
                }
                if (_waterview.ToString() == "True")
                {
                    _SearchWaterView = "YES";
                }


                StringBuilder _SearchCriteria = new StringBuilder();
                _SearchCriteria.Capacity = 500;

                if (_propertyType.ToString().Length > 0)
                {
                    _SearchCriteria.Append("Listing Type: <b>" + _propertyType.ToString() + "</b> &nbsp;");
                }
                if (_town.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Town: <b>" + _town.ToString() + "</b> &nbsp;");
                }
                if (_village.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Village: <b>" + _village.ToString() + "</b> &nbsp;");
                }
                if (_bedRooms > 0)
                {
                    _SearchCriteria.Append(" Bedrooms: <b>" + _bedRooms.ToString() + "</b> &nbsp;");
                }
                if (_bathRooms > 0)
                {
                    _SearchCriteria.Append(" Bathrooms: <b>" + _bathRooms.ToString() + "</b> &nbsp;");
                }
                if (_SearchWaterFront.ToString() == "YES")
                {
                    _SearchCriteria.Append(" Waterfront: <b>" + _SearchWaterFront.ToString() + "</b> &nbsp;");
                }
                if (_SearchWaterView.ToString() == "YES")
                {
                    _SearchCriteria.Append(" Waterview: <b>" + _SearchWaterView.ToString() + "</b> &nbsp;");
                }
                if (_priceLow > 0)
                {
                    _SearchCriteria.Append(" Min. Price: <b>" + _priceLow.ToString() + "</b> &nbsp;");
                }
                if (_priceHigh < 50000000)
                {
                    _SearchCriteria.Append(" Max Price: <b>" + _priceHigh.ToString() + "</b> &nbsp;");
                }
                if (_listingOfficeMLSID.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Office: <b>" + _listingOfficeMLSID.ToString() + "</b> &nbsp;");
                }
                //else
                //{
                //    _listingOfficeMLSID = "";
                //}

                if (_dom.ToString().Trim().Length > 0)
                {
                    _SearchCriteria.Append(" Days On Market: <b>" + _dom.ToString() + " day or less</b> &nbsp;");
                }
                //else
                //{
                //    _dom = "";
                //}

                if (_complex.ToString().Length > 0)
                {
                    _SearchCriteria.Append(" Complex: <b>" + _complex.ToString() + "</b> &nbsp;");
                }
                //else
                //{
                //    _complex = "";
                //}


                lblSearchCriteria.Text = _SearchCriteria.ToString();

                List<FlexMLSInfo> items;
                FlexMLSController controller = new FlexMLSController();


                if (_condoSearch == true)
                {
                    items = controller.FlexMLS_Search_Condo(_propertyType.ToString(),
                        _town.ToString(), _village.ToString(),
                        _bedRooms.ToString(), _bathRooms.ToString(),
                        _SearchWaterFront.ToString(),
                        _SearchWaterView.ToString(),
                        _priceLow.ToString(),
                        _priceHigh.ToString(),
                        _listingOfficeMLSID, _dom.ToString(), _complex.ToString());
                  //      lblDebug.Text = " Searching Condos";
                }
                else if (_searchMlsNumber == true)
                {
                    items = controller.FlexMLS_Search_MLS_Numbers(_MlsNumbers.ToString());
                    //     lblDebug.Text = " Searching MLS Numbers";
                }
                else
                {
                    items = controller.FlexMLS_Search(_propertyType.ToString(),
                        _town.ToString(), _village.ToString(),
                        _bedRooms.ToString(), _bathRooms.ToString(),
                        _SearchWaterFront.ToString(),
                        _SearchWaterView.ToString(),
                        _priceLow.ToString(),
                        _priceHigh.ToString(),
                        _listingOfficeMLSID, _dom.ToString());
                      //   lblDebug.Text = " Searching Search Criteria";
                         //lblDebug.Text += "<br />_propertyType: " + _propertyType.ToString() +
                         //               "<br />_town: " + _town.ToString() +
                         //               "<br />_village: " + _village.ToString() +
                         //               "<br />_bedRooms: " + _bedRooms.ToString() +
                         //               "<br />_bathRooms: " + _bathRooms.ToString() +
                         //               "<br />_SearchWaterFront: " + _SearchWaterFront.ToString() +
                         //               "<br />_SearchWaterView: " + _SearchWaterView.ToString() +
                         //               "<br />_priceLow: " + _priceLow.ToString() +
                         //               "<br />_priceHigh: " + _priceHigh.ToString() +
                         //               "<br />_listingOfficeMLSID: " + _listingOfficeMLSID.ToString() +
                         //               "<br />_dom: " + _dom.ToString();
                }

          //      items.Sort("ListingPrice desc",1);
                PagedDataSource pg = new PagedDataSource();
                pg.DataSource = items;
                pg.AllowPaging = true;
                
                pg.PageSize = Int32.Parse(_numberOfRecords.ToString());

                pg.CurrentPageIndex = CurrentPage;

                lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + pg.PageCount.ToString();

                // Disable Prev or Next buttons if necessary

                lbPrev.Enabled = !pg.IsFirstPage;
                lbNext.Enabled = !pg.IsLastPage;

                
                //bind the data
                lstContent.DataSource = pg;
                lstContent.DataBind();

                lblSearchSummary.Text = "Total Listings Found: " + items.Count.ToString();



            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }

        //public void LoadTrending()
        //{

        //    try
        //    {
        //        //   string year = DateTime.Now.Year.ToString();


        //        List<FlexMLSInfo> items;
        //        FlexMLSController controller = new FlexMLSController();

        //        items = controller.FlexMLS_ListingViews_Get(100);

        //        //creating the PagedDataSource instance....
        //        PagedDataSource pg = new PagedDataSource();
        //        pg.DataSource = items;
        //        pg.AllowPaging = true;
        //        pg.PageSize = Int32.Parse(_numberOfRecords.ToString());

        //        pg.CurrentPageIndex = CurrentPage;

        //        lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + pg.PageCount.ToString();

        //        // Disable Prev or Next buttons if necessary

        //        lbPrev.Enabled = !pg.IsFirstPage;
        //        lbNext.Enabled = !pg.IsLastPage;



        //        //Binding pg to datalist


        //        //bind the data
        //        lstContent.DataSource = pg;
        //        lstContent.DataBind();



        //    }
        //    catch (Exception ex)
        //    {
        //        Exceptions.ProcessModuleLoadException(this, ex);
        //    }

        //}

        public void LoadSettings()
        {

            try
            {


                //if (Settings.Contains("ThumbImageAlign"))
                //{
                //    _imageAlign = Settings["ThumbImageAlign"].ToString();
                //}
                //if (Settings.Contains("MaxThumbSize"))
                //{
                //    _maxThumbSize = Settings["MaxThumbSize"].ToString();
                //}
                if (Settings.Contains("MLSImagesUrl"))
                {
                    _MLSImagesURL = Settings["MLSImagesUrl"].ToString();
                }

                if (Settings.Contains("FlexMLSPage"))
                {
                    _FlexMLSPage = Settings["FlexMLSPage"].ToString();
                }
                
                if (Settings.Contains("NumberOfRecords"))
                {
                    _numberOfRecords = Settings["NumberOfRecords"].ToString();
                }

                if (Settings.Contains("ShowPaging"))
                {
                    if (Convert.ToBoolean(Settings["ShowPaging"].ToString()) == false)
                    {
                        Paging_Data.Visible = false;
                    }
                }



                if (Settings.Contains("ShowCriteria"))
                {
                    divSearchCriteria.Visible = Convert.ToBoolean(Settings["ShowCriteria"].ToString());
                }

                if (Settings.Contains("ShowTotalRecords"))
                {
                    lblSearchSummary.Visible = Convert.ToBoolean(Settings["ShowTotalRecords"].ToString());
                }

                if (Settings.Contains("ListingOfficeMLSID"))
                {
                    _listingOfficeMLSID = Settings["ListingOfficeMLSID"].ToString();
                }

                if (Settings.Contains("PropertyType"))
                {
                    _propertyType = Settings["PropertyType"].ToString();
                }

                if (Settings.Contains("Town"))
                {
                    _town = Settings["Town"].ToString();
                }

                if (Settings.Contains("Village"))
                {
                    _village = Settings["Village"].ToString();
                }

                if (Settings.Contains("Bedrooms"))
                {
                    _beds = Settings["Bedrooms"].ToString();
                }

                if (Settings.Contains("Bathrooms"))
                {
                    _baths = Settings["Bathrooms"].ToString();
                }

                if (Settings.Contains("PriceLow"))
                {
                    _pricelow = Settings["PriceLow"].ToString();
                }

                if (Settings.Contains("PriceHigh"))
                {
                    _pricehigh = Settings["PriceHigh"].ToString();
                }

                if (Settings.Contains("WaterFront"))
                {
                    _waterfront = Settings["WaterFront"].ToString();
                }

                if (Settings.Contains("Waterview"))
                {
                    _waterview = Settings["Waterview"].ToString();
                }

                //if (Settings.Contains("Complex"))
                //{
                //    _complex = Settings["Complex"].ToString();
                //    if (_complex != null ||_complex.ToString().Length > 0)
                //    {
                //        _condoSearch = true;
                //    }
                //}

                if (Settings.Contains("DOM"))
                {
                    _dom = Settings["DOM"].ToString();
                }	




            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }


        //protected void lstContent_ItemDataBound(object sender, DataListItemEventArgs e)
        protected void lstContent_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            string _UnitNumber = "";
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink Line1 = (HyperLink)e.Item.FindControl("hyperLink1");
                HyperLink Line2 = (HyperLink)e.Item.FindControl("hyperLink2");

                //     Literal maxThumbSize = (Literal)e.Item.FindControl("LiteralmaxThumbNailSize");
                //     maxThumbSize.Text = _maxThumbSize.ToString();

                
                if (DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString().Length >= 1 && DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString() != "0")
                {
                    _UnitNumber = " #" + DataBinder.Eval(e.Item.DataItem, "UnitNumber").ToString();
                }

                string _content = Server.HtmlDecode(DataBinder.Eval(e.Item.DataItem, "Address").ToString() + _UnitNumber.ToString() + ", " + DataBinder.Eval(e.Item.DataItem, "Village").ToString());
                string _ListingNumber = DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString();
                string _listingprice = DataBinder.Eval(e.Item.DataItem, "ListingPrice").ToString();
                _listingprice = String.Format("{0:C0}", double.Parse(_listingprice)); // FORMAT CURRENCY


                string _pageName = _content.ToString().Replace(" ", "_").ToString().Replace("&", "").ToString().Replace(",", "").ToString() + ".aspx";
                string vLink = Globals.NavigateURL(Int32.Parse(_FlexMLSPage.ToString()));
                var result = vLink.Substring(vLink.LastIndexOf('/') + 1);
                // DISABLE ADDING OF NEW RECORD IF COMING FROM THIS MODULE BY QUERYSTRING ADDITION OF . . . /t/f
                vLink = vLink.ToString().Replace(result.ToString(), "tabid/" + _FlexMLSPage.ToString() + "/pg/v/t/f/MLS/" + _ListingNumber.ToString() + "/" + _pageName.ToString());
                Line1.NavigateUrl = vLink.ToString();
                Line2.NavigateUrl = vLink.ToString();

                Line1.Text = _content.ToString();
                string _details = DataBinder.Eval(e.Item.DataItem, "YearBuilt").ToString() + " " +
                    DataBinder.Eval(e.Item.DataItem, "Style").ToString() + " - " +
                    DataBinder.Eval(e.Item.DataItem, "Bedrooms").ToString() + " Beds, " +
                    DataBinder.Eval(e.Item.DataItem, "TotalBaths").ToString() + " Baths";
                //+ DataBinder.Eval(e.Item.DataItem, "LivingSpace").ToString() + " SQFT.";
//                    DataBinder.Eval(e.Item.DataItem, "ListingNumber").ToString() + "" + 
                Line2.Text = _details.ToString() + "<br />" + "MLS# " + _ListingNumber.ToString() + " - " + _listingprice.ToString();
                //YearBuilt Style LivingSpace Bedrooms  TotalBaths StatusCode
                string checkImage = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";
                Image ListingImage = (Image)e.Item.FindControl("imgListingImage");
                
                // HyperLinkImage
              //  HyperLink ImageLink = (HyperLink)e.Item.FindControl("HyperLinkImage");
              //  ImageLink.NavigateUrl = vLink.ToString();

                if (UrlExists(checkImage.ToString()) == true)
                {
                    //ListingImage.ImageUrl = "~/DesktopModules/GIBS/FlexMLS/ImageHandler.ashx?MlsNumber=" + _ListingNumber.ToString() + "&MaxSize=" + _maxThumbSize.ToString();        //checkImage.ToString();
                    ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + ".jpg";

                    ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString() + " - " + _content.ToString();
                    ListingImage.ToolTip = "MLS " + _ListingNumber.ToString() + " - " + _content.ToString();
                    //if (_imageAlign.ToString().ToLower() == "left")
                    //{
                    //    ListingImage.ImageAlign = ImageAlign.Left;
                    //    ListingImage.Attributes.Add("style", "margin-right:5px;");
                    //}
                    //else
                    //{
                    //    ListingImage.ImageAlign = ImageAlign.Right;
                    //    ListingImage.Attributes.Add("style", "margin-left:5px;");
                    //}

                }
                else if (UrlExists(_MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg") == true)
                {
                    //
                    ListingImage.ImageUrl = _MLSImagesURL.ToString() + _ListingNumber.ToString() + "_1.jpg";
                    ListingImage.AlternateText = "MLS Listing " + _ListingNumber.ToString() + " - " + _content.ToString();
                    ListingImage.ToolTip = "MLS " + _ListingNumber.ToString() + " - " + _content.ToString();
                    //if (_imageAlign.ToString().ToLower() == "left")
                    //{
                    //    ListingImage.ImageAlign = ImageAlign.Left;
                    //    ListingImage.Attributes.Add("style", "margin-right:5px;");
                    //}
                    //else
                    //{
                    //    ListingImage.ImageAlign = ImageAlign.Right;
                    //    ListingImage.Attributes.Add("style", "margin-left:5px;");
                    //}

                }

                else
                {
                    ListingImage.Visible = false;
                    // ListingImage.ImageUrl = "http://mls.gibs.com/images/NoImage.jpg";

                    //    return "";
                }

            }
            //       GetImage(_ListingNumber.ToString());

        }



        protected void lbPrev_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the previous page
            //  CurrentPage = (Int32.Parse(hidPage.Value.ToString()) - 1);
            CurrentPage -= 1;


            hidPage.Value = CurrentPage.ToString();
            // Reload control

            SearchMLS();
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            // Set viewstate variable to the next page
            //CurrentPage = (Int32.Parse(hidPage.Value.ToString()) + 1);
            CurrentPage += 1;

            hidPage.Value = CurrentPage.ToString();
            // Reload control
            SearchMLS();
        }






        private static bool UrlExists(string url)
        {
            try
            {
                new System.Net.WebClient().DownloadData(url);
                return true;
            }
            catch (System.Net.WebException e)
            {
                if (((System.Net.HttpWebResponse)e.Response).StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;
                else
                    throw;
            }
        }

        #region IActionable Members

        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //create a new action to add an item, this will be added to the controls
                //dropdown menu
                ModuleActionCollection actions = new ModuleActionCollection();
                //actions.Add(GetNextActionID(), Localization.GetString(ModuleActionType.AddContent, this.LocalResourceFile),
                //    ModuleActionType.AddContent, "", "", EditUrl(), false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                //     true, false);

                return actions;
            }
        }

        #endregion


        /// <summary>
        /// Handles the items being bound to the datalist control. In this method we merge the data with the
        /// template defined for this control to produce the result to display to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

    }
}
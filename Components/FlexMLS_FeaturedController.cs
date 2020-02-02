
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Search;
//using DotNetNuke.Common.Utilities;
//using DotNetNuke.Entities.Modules;
//using DotNetNuke.Services.Search;

namespace GIBS.Modules.FlexMLS_Featured.Components
{
    public class FlexMLS_FeaturedController : ISearchable, IPortable
    {

        #region public method

        /// <summary>
        /// Gets all the FlexMLS_FeaturedInfo objects for items matching the this moduleId
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<FlexMLS_FeaturedInfo> GetFlexMLS_Featureds(int moduleId)
        {
            return CBO.FillCollection<FlexMLS_FeaturedInfo>(DataProvider.Instance().GetFlexMLS_Featureds(moduleId));
        }

        /// <summary>
        /// Get an info object from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public FlexMLS_FeaturedInfo GetFlexMLS_Featured(int moduleId, int itemId)
        {
            return (FlexMLS_FeaturedInfo)CBO.FillObject(DataProvider.Instance().GetFlexMLS_Featured(moduleId, itemId), typeof(FlexMLS_FeaturedInfo));
        }


        /// <summary>
        /// Adds a new FlexMLS_FeaturedInfo object into the database
        /// </summary>
        /// <param name="info"></param>
        public void AddFlexMLS_Featured(FlexMLS_FeaturedInfo info)
        {
            //check we have some content to store
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().AddFlexMLS_Featured(info.ModuleId, info.Content, info.CreatedByUser);
            }
        }

        /// <summary>
        /// update a info object already stored in the database
        /// </summary>
        /// <param name="info"></param>
        public void UpdateFlexMLS_Featured(FlexMLS_FeaturedInfo info)
        {
            //check we have some content to update
            if (info.Content != string.Empty)
            {
                DataProvider.Instance().UpdateFlexMLS_Featured(info.ModuleId, info.ItemId, info.Content, info.CreatedByUser);
            }
        }


        /// <summary>
        /// Delete a given item from the database
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="itemId"></param>
        public void DeleteFlexMLS_Featured(int moduleId, int itemId)
        {
            DataProvider.Instance().DeleteFlexMLS_Featured(moduleId, itemId);
        }


        #endregion

        #region ISearchable Members

        /// <summary>
        /// Implements the search interface required to allow DNN to index/search the content of your
        /// module
        /// </summary>
        /// <param name="modInfo"></param>
        /// <returns></returns>
        public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(ModuleInfo modInfo)
        {
            SearchItemInfoCollection searchItems = new SearchItemInfoCollection();

            List<FlexMLS_FeaturedInfo> infos = GetFlexMLS_Featureds(modInfo.ModuleID);

            foreach (FlexMLS_FeaturedInfo info in infos)
            {
                SearchItemInfo searchInfo = new SearchItemInfo(modInfo.ModuleTitle, info.Content, info.CreatedByUser, info.CreatedDate,
                                                    modInfo.ModuleID, info.ItemId.ToString(), info.Content, "Item=" + info.ItemId.ToString());
                searchItems.Add(searchInfo);
            }

            return searchItems;
        }

        #endregion

        #region IPortable Members

        /// <summary>
        /// Exports a module to xml
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        public string ExportModule(int moduleID)
        {
            StringBuilder sb = new StringBuilder();

            List<FlexMLS_FeaturedInfo> infos = GetFlexMLS_Featureds(moduleID);

            if (infos.Count > 0)
            {
                sb.Append("<FlexMLS_Featureds>");
                foreach (FlexMLS_FeaturedInfo info in infos)
                {
                    sb.Append("<FlexMLS_Featured>");
                    sb.Append("<content>");
                    sb.Append(XmlUtils.XMLEncode(info.Content));
                    sb.Append("</content>");
                    sb.Append("</FlexMLS_Featured>");
                }
                sb.Append("</FlexMLS_Featureds>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// imports a module from an xml file
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="Content"></param>
        /// <param name="Version"></param>
        /// <param name="UserID"></param>
        public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        {
            XmlNode infos = DotNetNuke.Common.Globals.GetContent(Content, "FlexMLS_Featureds");

            foreach (XmlNode info in infos.SelectNodes("FlexMLS_Featured"))
            {
                FlexMLS_FeaturedInfo FlexMLS_FeaturedInfo = new FlexMLS_FeaturedInfo();
                FlexMLS_FeaturedInfo.ModuleId = ModuleID;
                FlexMLS_FeaturedInfo.Content = info.SelectSingleNode("content").InnerText;
                FlexMLS_FeaturedInfo.CreatedByUser = UserID;

                AddFlexMLS_Featured(FlexMLS_FeaturedInfo);
            }
        }

        #endregion
    }
}

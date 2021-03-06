﻿// <copyright>
// Copyright by the Spark Development Network
//
// Licensed under the Rock Community License (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.rockrms.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//
using System;
using System.Collections.Generic;
using System.Web.UI;
using Rock.Data;
using Rock.Model;
using Rock.Web.Cache;
using Rock.Web.UI;

namespace Rock.Badge
{
    /// <summary>
    /// Base class for person profile badges
    /// </summary>
    public abstract class BadgeComponent : Rock.Extension.Component
    {
        /// <summary>
        /// Determines of this badge component applies to the given type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public virtual bool DoesApplyToEntityType( string type )
        {
            return true;
        }

        /// <summary>
        /// Gets the attribute value defaults.
        /// </summary>
        /// <value>
        /// The attribute defaults.
        /// </value>
        public override Dictionary<string, string> AttributeValueDefaults
        {
            get
            {
                var defaults = new Dictionary<string, string>();
                defaults.Add( "Active", "True" );
                defaults.Add( "Order", "0" );
                return defaults;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public override bool IsActive
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public override int Order
        {
            get
            {
                return 0;
            }
        }


        /// <summary>
        /// Gets or sets the parent context block.
        /// </summary>
        public ContextEntityBlock ParentContextEntityBlock { get; set; }

        /// <summary>
        /// Gets or sets the parent person block.
        /// </summary>
        [RockObsolete( "1.10" )]
        [Obsolete( "Use the ParentContextEntityBlock instead.", false )]
        public PersonBlock ParentPersonBlock
        {
            get => ParentContextEntityBlock as PersonBlock;
        }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public virtual IEntity Entity { get; set; }

        /// <summary>
        /// Gets or sets the entity as a person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public Person Person
        {
            get => Entity as Person;
            set => Entity = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeComponent" /> class.
        /// </summary>
        public BadgeComponent() : base( false )
        {
            // Override default constructor of Component that loads attributes (needs to be done by each instance)
        }

        /// <summary>
        /// Loads the attributes for the badge.  The attributes are loaded by the framework prior to executing the badge, 
        /// so typically Person Badges do not need to load the attributes
        /// </summary>
        /// <param name="badge">The badge.</param>
        public void LoadAttributes( Model.Badge badge )
        {
            badge.LoadAttributes();
        }

        /// <summary>
        /// Use GetAttributeValue( BadgeCache badge, string key) instead.  Person Badge attribute values are 
        /// specific to the badge instance (rather than global).  This method will throw an exception
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Person Badge attributes are saved specific to the current badge, which requires that the current badge is included in order to load or retrieve values.  Use the GetAttributeValue( PersonBadge badge, string key ) method instead.</exception>
        public override string GetAttributeValue( string key )
        {
            throw new Exception( "Badge attributes are saved specific to the current badge, which requires that the current badge is included in order to load or retrieve values.  Use the GetAttributeValue( BadgeCache badge, string key ) method instead." );
        }

        /// <summary>
        /// Gets the attribute value for the badge
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected string GetAttributeValue( BadgeCache badge, string key )
        {
            return badge.GetAttributeValue( key );
        }

        /// <summary>
        /// Gets the attribute value for the badge
        /// </summary>
        /// <param name="personBadgeCache">The badge.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        [RockObsolete( "1.10" )]
        [Obsolete( "Use the BadgeCache param instead.", false )]
        protected string GetAttributeValue( PersonBadgeCache personBadgeCache, string key )
        {
            var badgeCache = BadgeCache.Get( personBadgeCache.Id );
            return GetAttributeValue( badgeCache, key );
        }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="writer">The writer.</param>
        public virtual void Render( BadgeCache badge, HtmlTextWriter writer ) { }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="personBadgeCache">The badge.</param>
        /// <param name="writer">The writer.</param>
        [RockObsolete( "1.10" )]
        [Obsolete( "Use the BadgeCache param instead.", false )]
        public virtual void Render( PersonBadgeCache personBadgeCache, HtmlTextWriter writer ) { }
    }
}

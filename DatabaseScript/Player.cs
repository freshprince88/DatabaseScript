using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="Fakultät für Sport- und Gesundheitswissenschaft">
//    Copyright © 2013, 2014 Fakultät für Sport- und Gesundheitswissenschaft
// </copyright>
//-------------------------------------------------------------------

namespace DatabaseScript
{
    using System;
    using System.Xml.Serialization;


    /// <summary>
    /// A player participating in a <see cref="Match"/>.
    /// </summary>
    public class Player
    {

        /// <summary>
        /// Backs the <see cref="Name"/> property.
        /// </summary>
        private string name;

        /// <summary>
        /// Backs the <see cref="FirstName"/> property.
        /// </summary>
        private string firstName;

        /// <summary>
        /// Backs the <see cref="Nationality"/> property.
        /// </summary>
        private string nationality;

        /// <summary>
        /// Backs the <see cref="Rank"/> property.
        /// </summary>
        private Rank rank;

        /// <summary>
        /// Backs the <see cref="PlayingStyle"/> property.
        /// </summary>
        private PlayingStyle playingStyle = PlayingStyle.None;

        /// <summary>
        /// Backs the <see cref="Handedness"/> property.
        /// </summary>
        private Handedness handedness = Handedness.None;

        /// <summary>
        /// Backs the <see cref="Grip"/> property.
        /// </summary>
        private Grip grip = Grip.None;
        /// <summary>
        /// Backs the <see cref="StartingTableEnd"/> property.
        /// </summary>
        private StartingTableEnd startingTableEnd = StartingTableEnd.None;

        /// <summary>
        /// Backs the <see cref="Material"/> property.
        /// </summary>
        private string material;

        /// <summary>
        /// Backs the <see cref="MaterialBH"/> property.
        /// </summary>
        private MaterialBH materialBH;
        /// <summary>
        /// Backs the <see cref="MaterialFH"/> property.
        /// </summary>
        private MaterialFH materialFH;

        private MatchPlayer playerIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.rank = new Rank(0, DateTime.Today);
        }

        public Player(int index) : this()
        {
            this.name = string.Format(Properties.Resources.player_name_default, index);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        public Player(string name)
            : this(name, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="nationality">The nationality of the player.</param>
        public Player(string name, string nationality)
        {
            this.Name = name;
            this.Nationality = nationality;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="firstName">The first name of the player.</param>

        /// <param name="nationality">The nationality of the player.</param>
        public Player(string name,string firstName, string nationality)
        {
            this.Name = name;
            this.FirstName = firstName;
            this.Nationality = nationality;

        }

        /// <summary>
        /// Gets or sets the last name of this player.
        /// </summary>
        [XmlAttribute]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the last first name of this player.
        /// </summary>
        [XmlAttribute]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the PlayingStyle of this player.
        /// </summary>
        [XmlAttribute]
        public PlayingStyle PlayingStyle
        {
            get
            {
                return this.playingStyle;
            }

            set
            {
                if (this.playingStyle != value)
                {
                    this.playingStyle = value;
                }
            }
        }


        /// <summary>
        /// Gets or sets the StartingTableEnd of this player.
        /// </summary>
        [XmlAttribute]
        public StartingTableEnd StartingTableEnd
        {
            get
            {
                return this.startingTableEnd;
            }

            set
            {
                if (this.startingTableEnd != value)
                {
                    this.startingTableEnd = value;

                }
            }
        }

        /// <summary>
        /// Gets or sets the Handedness of this player.
        /// </summary>
        [XmlAttribute]
        public Handedness Handedness
        {
            get
            {
                return this.handedness;
            }

            set
            {
                if (this.handedness != value)
                {
                    this.handedness = value;

                }
            }
        }

        /// <summary>
        /// Gets or sets the Grip of this player.
        /// </summary>
        [XmlAttribute]
        public Grip Grip
        {
            get
            {
                return this.grip;
            }

            set
            {
                if (this.grip != value)
                {
                    this.grip = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the PlayerIndex of this player.
        /// </summary>
        [XmlAttribute]
        public MatchPlayer PlayerIndex
        {
            get
            {
                return this.playerIndex;
            }

            set
            {
                if (this.playerIndex != value)
                {
                    this.playerIndex = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Material in the Forehand of this player.
        /// </summary>
        [XmlAttribute]
        public MaterialFH MaterialFH
        {
            get
            {
                return this.materialFH;
            }

            set
            {
                if (this.materialFH != value)
                {
                    this.materialFH = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the Material in the Backhand of this player.
        /// </summary>
        [XmlAttribute]
        public MaterialBH MaterialBH
        {
            get
            {
                return this.materialBH;
            }

            set
            {
                if (this.materialBH != value)
                {
                    this.materialBH = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Material of this player.
        /// </summary>
        [XmlAttribute]
        public string Material
        {
            get
            {
                return this.material;
            }

            set
            {
                if (this.material != value)
                {
                    this.material = value;

                }
            }
        }

        /// <summary>
        /// Gets or sets the nationality of this player.
        /// </summary>
        [XmlAttribute]
        public string Nationality
        {
            get
            {
                return this.nationality;
            }

            set
            {
                if (this.nationality != value)
                {
                    this.nationality = value;


                }
            }
        }

        /// <summary>
        /// Gets or sets the rank of this player.
        /// </summary>
        public Rank Rank
        {
            get
            {
                return this.rank;
            }

            set
            {
                if (this.rank != value)
                {
                    this.rank = value;

                }
            }
        }

        /// <summary>
        /// Gets a human-readable name of the player, including nationality and rank.
        /// </summary>
        [XmlIgnore]
        public string FullName
        {
            get
            {
                var nationality = !string.IsNullOrEmpty(this.Nationality) ?
                    this.Nationality : "unknown nationality";
                var rank = this.Rank != null && this.Rank.Position > 0 ?
                    this.Rank.ToString() : "no ranking";
                return string.Format("{0} ({1}, {2})", this.Name, nationality, rank);
            }
        }

        /// <summary>
        /// Gets a human-readable string representation of this player.
        /// </summary>
        /// <returns>A string representation of this player.</returns>
        public override string ToString()
        {
            return this.FullName;
        }
    }
}

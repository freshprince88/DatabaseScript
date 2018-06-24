using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DatabaseScript
{
    public class MatchEntry
    {
        /// <summary>
        /// Backs the GUID of the Match
        /// </summary>
        private Guid id;

        /// <summary>
        /// Backs the <see cref="FirstPlayer"/> property.
        /// </summary>
        private Player firstPlayer;

        /// <summary>
        /// Backs the <see cref="SecondPlayer"/> property.
        /// </summary>
        private Player secondPlayer;

        /// <summary>
        /// Backs the <see cref="DateTime"/> property.
        /// </summary>
        private DateTime dateTime;

        /// <summary>
        /// Backs the <see cref="Tournament"/> property.
        /// </summary>
        private string tournament;

        /// <summary>
        /// Backs the <see cref="Year"/> property.
        /// </summary>
        private int year;
        /// <summary>
        /// Backs the <see cref="Category"/> property.
        /// </summary>
        private MatchCategory category = MatchCategory.Category;
        /// <summary>
        /// Backs the <see cref="Sex"/> property.
        /// </summary>
        private MatchSex sex = MatchSex.Sex;


        /// <summary>
        /// Backs the <see cref="Class"/> property.
        /// </summary>
        private DisabilityClass disabilityClass = DisabilityClass.Class;

        /// <summary>
        /// Backs the <see cref="Round"/> property.
        /// </summary>
        private MatchRound round = MatchRound.Round;

        /// <summary>
        /// Backs the <see cref="Mode"/> property.
        /// </summary>
        private MatchMode mode = MatchMode.BestOf5;


        public MatchEntry()
        {

        }

        /// <summary>
        ///  Gets the Unique ID of this match
        /// </summary>
        public Guid ID
        {
            get { return this.id; }
        }

        /// <summary>
        /// Gets or sets the first <see cref="Player"/> in this match.
        /// </summary>
        public Player FirstPlayer
        {
            get { return this.firstPlayer; }
            set { this.firstPlayer=value; }
        }

        /// <summary>
        /// Gets or sets the second <see cref="Player"/> in this match.
        /// </summary>
        public Player SecondPlayer
        {
            get { return this.secondPlayer; }
            set { this.secondPlayer=value; }
        }


        /// <summary>
        /// Gets all players in this match.
        /// </summary>
        [XmlIgnore]
        public IEnumerable<Player> Players
        {
            get
            {
                if (this.firstPlayer != null)
                {
                    yield return this.firstPlayer;
                }

                if (this.secondPlayer != null)
                {
                    yield return this.secondPlayer;
                }
            }
        }

        /// <summary>
        /// Gets or sets the tournament the match is part of.
        /// </summary>
        [XmlAttribute]
        public string Tournament
        {
            get { return this.tournament; }
            set { this.tournament=value; }
        }

        /// <summary>
        /// Gets or sets the year of the tournament.
        /// </summary>
        [XmlAttribute]
        public int Year
        {
            get { return this.year; }
            set { this.year = value; }
        }

        /// <summary>
        /// Gets or sets the Video location the match is part of.
        /// </summary>


        /// <summary>
        /// Gets or sets the category of the match.
        /// </summary>
        [XmlAttribute]
        public MatchCategory Category
        {
            get { return this.category; }
            set { this.category=value; }
        }
        /// <summary>
        /// Gets or sets the sex of the players.
        /// </summary>
        [XmlAttribute]
        public MatchSex Sex
        {
            get { return this.sex; }
            set { this.sex = value; }
        }

        /// <summary>
        /// Gets or sets the Disability Class of the Players.
        /// </summary>
        [XmlAttribute]
        public DisabilityClass DisabilityClass
        {
            get { return this.disabilityClass; }
            set { this.disabilityClass=value; }
        }

        /// <summary>
        /// Gets or sets the round of the match.
        /// </summary>
        [XmlAttribute]
        public MatchRound Round
        {
            get { return this.round; }
            set { this.round=value; }
        }

        /// <summary>
        /// Gets or sets the mode of this match.
        /// </summary>
        [XmlAttribute]
        public MatchMode Mode
        {
            get { return this.mode; }
            set { this.mode=value; }
        }

        /// <summary>
        /// Gets or sets the date and time of the match.
        /// </summary>
        [XmlAttribute]
        public DateTime DateTime
        {
            get { return this.dateTime; }
            set { this.dateTime=value; }
        }


    }
}

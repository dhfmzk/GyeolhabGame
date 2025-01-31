namespace Domain
{
    public record AnswerData
    {
        public static readonly int Invalid = -1;

        public int Answer1;
        public int Answer2;
        public int Answer3;

        public bool IsCompleted => this.Contain(AnswerData.Invalid);
        
        public bool Contain(int value)
        {
            return this.Answer1 == value || this.Answer2 == value || this.Answer3 == value;
        }

        public void Reset()
        {
            this.Answer1 = -1;
            this.Answer2 = -1;
            this.Answer3 = -1;
        }

        public bool Add(int value)
        {
            if (this.Answer1 == Invalid)
            {
                this.Answer1 = value;
                return true;
            }
            
            if (this.Answer2 == Invalid)
            {
                this.Answer2 = value;
                return true;
            }
            
            if (this.Answer3 == Invalid)
            {
                this.Answer3 = value;
                return true;
            }

            return false;
        }

        public bool Remove(int value)
        {
            if (this.Answer1 == value)
            {
                this.Answer1 = Invalid;
                return true;
            }
            
            if (this.Answer2 == value)
            {
                this.Answer2 = Invalid;
                return true;
            }
            
            if (this.Answer3 == value)
            {
                this.Answer3 = Invalid;
                return true;
            }
            
            return false;
        }

        public bool Equals(int answer1, int answer2, int answer3)
        {
            if (answer1 == answer2 ||
                answer1 == answer3 || 
                answer2 == answer3)
            {
                return false;
            }
            
            return (this.Answer1 == answer1 || this.Answer2 == answer1 || this.Answer3 == answer1) &&
                   (this.Answer1 == answer2 || this.Answer2 == answer2 || this.Answer3 == answer2) &&
                   (this.Answer1 == answer3 || this.Answer2 == answer3 || this.Answer3 == answer3);
        }
    }
}
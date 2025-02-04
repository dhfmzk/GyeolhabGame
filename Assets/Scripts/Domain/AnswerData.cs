using System;
using System.Linq;

namespace Domain
{
    public record AnswerData
    {
        public static readonly int Invalid = -1;

        private int[] answers;

        public bool IsCompleted => !this.Contains(AnswerData.Invalid);

        public AnswerData()
        {
            this.Reset();
        }

        public bool Contains(int value)
        {
            return this.answers.Contains(value);
        }

        public void Reset()
        {
            this.answers = new[] { Invalid, Invalid, Invalid };
        }

        public bool Add(int value)
        {
            for (var i = 0; i < this.answers.Length; i++)
            {
                if (this.answers[i] != Invalid)
                {
                    continue;
                }
                
                this.answers[i] = value;
                return true;
            }

            return false;
        }

        public bool Remove(int value)
        {
            for (var i = 0; i < this.answers.Length; i++)
            {
                if (this.answers[i] != value)
                {
                    continue;
                }
                
                this.answers[i] = Invalid;
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

            return this.Contains(answer1) && this.Contains(answer2) && this.Contains(answer3);
        }
        
        public bool Equals(in AnswerData answer)
        {
            if (answer == null)
            {
                return false;
            }

            foreach (var item in this.answers)
            {
                if (!answer.answers.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }
        
        public override string ToString()
        {
            return $"[AnswerData : {this.answers[0]}, {this.answers[1]}, {this.answers[2]}]";
        }
    }
}
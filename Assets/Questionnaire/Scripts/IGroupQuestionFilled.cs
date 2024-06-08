using System;

namespace TeachR.Measurements
{
    [Serializable]
    public class IGroupQuestionFilled
    {
        public string name;
        public string question;
        public string anchorLeft;
        public string anchorMiddle;
        public string anchorRight;
        public int value;

        public IGroupQuestionFilled(IGroupQuestion iGroupQuestion, int value)
        {
            name = iGroupQuestion.name;
            question = iGroupQuestion.question;
            anchorLeft = iGroupQuestion.anchorLeft;
            anchorMiddle = iGroupQuestion.anchorMiddle;
            anchorRight = iGroupQuestion.anchorRight;
            this.value = value;
        }
    }
}
namespace YuGiOh {

    public abstract class SetCardOrientationAction : CardAction {
        CardOrientation orientation;

        protected SetCardOrientationAction(CardOrientation orientation) {
            this.orientation = orientation;
        }

        public override bool Check(Game g, CardEntity c) {
            return (c.zone.ValidOrientations() & orientation) != 0;
        }
        public override void Execute(Game g, CardEntity c) {
            c.orientation = orientation;
        }
        public override CardAction GetUndo(Game g, CardEntity c) {
            switch (c.orientation) {
                case CardOrientation.faceup:
                    return CardAction<ToFaceupAction>.action;
                case CardOrientation.facedown:
                    return CardAction<ToFacedownAction>.action;
                case CardOrientation.faceupideways:
                    return CardAction<ToFaceupSidewaysAction>.action;
                case CardOrientation.facedownsideways:
                    return CardAction<ToFacedownSidewaysAction>.action;
            }

            return null;
        }
    }


    //**********************************************************************************************************************
    // Implementations
    //**********************************************************************************************************************


    public class ToFaceupAction : SetCardOrientationAction {
        public override string Name(Game g, CardEntity c) => "To Faceup";
        public override string DisplayText(Game g, CardEntity c) => "Set card to faceup";

        public ToFaceupAction() : base(CardOrientation.faceup) { }
    }

    public class ToFacedownAction : SetCardOrientationAction {
        public override string Name(Game g, CardEntity c) => "To Facedown";
        public override string DisplayText(Game g, CardEntity c) => "Set card to facedown";

        public ToFacedownAction() : base(CardOrientation.facedown) { }
    }
    public class ToFaceupSidewaysAction : SetCardOrientationAction {
        public override string Name(Game g, CardEntity c) => "To Faceup Sideways";
        public override string DisplayText(Game g, CardEntity c) => "Set card to faceup sideways";

        public ToFaceupSidewaysAction() : base(CardOrientation.faceupideways) { }
    }

    public class ToFacedownSidewaysAction : SetCardOrientationAction {
        public override string Name(Game g, CardEntity c) => "To Facedown Sideways";
        public override string DisplayText(Game g, CardEntity c) => "Set card to facedown sideways";

        public ToFacedownSidewaysAction() : base(CardOrientation.facedownsideways) { }
    }

}
using YuGiOh;

public class NewCardActionState : GameState {
    public NewCardActionState(GameStateController gsc) : base(gsc) { }


    CardEntity card;

    public void SetCard(CardEntity c) {
        card = c;
        controller.OpenCardActionMenu(card);
        references.cardActionMenuBehaviour.SetPostionCenter();
    }


    public override GameState Next() {
        if (InputManager.VoiceCommandRecieved.yes) {
            var vc = InputManager.VoiceCommandRecieved.command;

            if (vc == VoiceCommand.SummonMonsterAtk) {
                var z = ToZoneCardAction.Get(controller.g, card, ZoneType.monster);
                var a = SetCardOrientationAction.Get(card, CardOrientation.faceup);
                
                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
            else if(vc == VoiceCommand.SummonMonsterDef) {
                var z = ToZoneCardAction.Get(controller.g, card, ZoneType.monster);
                var a = SetCardOrientationAction.Get(card, CardOrientation.faceupSideways);

                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
            else if(vc == VoiceCommand.SummonMonsterSet) {
                var z = ToZoneCardAction.Get(controller.g, card, ZoneType.monster);
                var a = SetCardOrientationAction.Get(card, CardOrientation.facedownsideways);

                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
            else if(vc == VoiceCommand.SetSpell) {
                var z = ToZoneCardAction.Get(controller.g, card, ZoneType.spell);
                var a = SetCardOrientationAction.Get(card, CardOrientation.facedown);

                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
            else if(vc == VoiceCommand.DiscardToGraveyard) {
                var z = ToZoneCardAction.Get(controller.g, card, ZoneType.graveyard);
                var a = SetCardOrientationAction.Get(card, CardOrientation.faceup);

                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
        }

        if (InputManager.ActionRequested.yes) {
            actionStack.AddExecute(InputManager.ActionRequested.action);
            references.cardActionMenuBehaviour.ClearOptions();
            InputManager.Set.ActionUsed();
            return controller.WaitForInputState();
        }

        return null;
    }
}

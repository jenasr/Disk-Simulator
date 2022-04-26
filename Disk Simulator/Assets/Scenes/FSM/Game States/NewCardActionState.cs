﻿using YuGiOh;

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
                var a = SetCardOrientationAction.Get(card, CardOrientation.facedownsideways);

                actionStack.AddExecute(z);
                actionStack.AddExecute(a);

                references.cardActionMenuBehaviour.ClearOptions();
                return controller.WaitForInputState();
            }
        }

        if (InputManager.ActionRequested.yes) {
            actionStack.AddExecute(InputManager.ActionRequested.action);
            references.cardActionMenuBehaviour.ClearOptions();
            return controller.WaitForInputState();
        }

        return null;
    }
}
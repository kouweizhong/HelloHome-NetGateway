namespace HelloHome.Common.Entities
{
    public abstract class Action
    {
        public int Id { get; set; }
        public int TriggerId { get; set; }
        public Trigger Trigger { get; set; }
        public int Sequence { get; set; }
    }

    public abstract class RelayAction : Action
    {
        public int RelayActuatorGroupId { get; set; }
        public RelayActuatorGroup RelayActuatorGroup { get; set; }

    }

    public class TurnOnAction : RelayAction
    {
    }

    public class TurnOffAction : RelayAction
    {
    }
}
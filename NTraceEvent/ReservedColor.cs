namespace NTraceEvent
{
    public readonly record struct ReservedColor(string Key)
    {
        public static readonly ReservedColor ThreadStateUninterruptible = new("thread_state_uninterruptible");
        public static readonly ReservedColor ThreadStateIOWait = new("thread_state_iowait");
        public static readonly ReservedColor ThreadStateRunning = new("thread_state_running");
        public static readonly ReservedColor ThreadStateRunnable = new("thread_state_runnable");
        public static readonly ReservedColor ThreadStateSleeping = new("thread_state_sleeping");
        public static readonly ReservedColor ThreadStateUnknown = new("thread_state_unknown");

        //background_memory_dump: new tr.b.Color(0, 180, 180),
        //light_memory_dump: new tr.b.Color(0, 0, 180),
        //detailed_memory_dump: new tr.b.Color(180, 0, 180),

        //vsync_highlight_color: new tr.b.Color(0, 0, 255),
        //generic_work: new tr.b.Color(125, 125, 125),

        //good: new tr.b.Color(0, 125, 0),
        //bad: new tr.b.Color(180, 125, 0),
        //terrible: new tr.b.Color(180, 0, 0),

        //black: new tr.b.Color(0, 0, 0),
        //grey: new tr.b.Color(221, 221, 221),
        //white: new tr.b.Color(255, 255, 255),
        //yellow: new tr.b.Color(255, 255, 0),
        //olive: new tr.b.Color(100, 100, 0),

        //rail_response: new tr.b.Color(67, 135, 253),
        //rail_animation: new tr.b.Color(244, 74, 63),
        //rail_idle: new tr.b.Color(238, 142, 0),
        //rail_load: new tr.b.Color(13, 168, 97),
        //startup: new tr.b.Color(230, 230, 0),

        //heap_dump_stack_frame: new tr.b.Color(128, 128, 128),
        //heap_dump_object_type: new tr.b.Color(0, 0, 255),
        //heap_dump_child_node_arrow: new tr.b.Color(204, 102, 0),

        //cq_build_running: new tr.b.Color(255, 255, 119),
        //cq_build_passed: new tr.b.Color(153, 238, 102),
        //cq_build_failed: new tr.b.Color(238, 136, 136),
        //cq_build_abandoned: new tr.b.Color(187, 187, 187),

        //cq_build_attempt_runnig: new tr.b.Color(222, 222, 75),
        //cq_build_attempt_passed: new tr.b.Color(103, 218, 35),
        //cq_build_attempt_failed: new tr.b.Color(197, 81, 81)
    }
}
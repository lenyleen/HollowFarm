namespace Views.GameViews.OutlineManager
{
    public class OutlineManager
    {
        /*public class OutlineManager : MonoBehaviour
{
    [System.Serializable]
    public class OutlineData
    {
        public Renderer renderer;
        public Color color = Color.yellow;
        public float thickness = 0.005f;
    }
    
    [SerializeField] private OutlineData[] outlineObjects;
    private Dictionary<Renderer, MaterialPropertyBlock> propertyBlocks;
    private SignalBus signalBus;
    
    private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
    private static readonly int ThicknessID = Shader.PropertyToID("_Thickness");
    
    private void Start()
    {
        InitializeOutlines();
        
        // Подписываемся на сигналы
        signalBus = ProjectContext.Instance.Container.Resolve<SignalBus>();
        signalBus.Subscribe<OutlineChangeSignal>(OnOutlineChangeSignal);
        signalBus.Subscribe<GlobalOutlineChangeSignal>(OnGlobalOutlineChangeSignal);
    }
    
    private void InitializeOutlines()
    {
        propertyBlocks = new Dictionary<Renderer, MaterialPropertyBlock>();
        
        foreach (var data in outlineObjects)
        {
            if (data.renderer != null)
            {
                var block = new MaterialPropertyBlock();
                block.SetColor(OutlineColorID, data.color);
                block.SetFloat(ThicknessID, data.thickness);
                data.renderer.SetPropertyBlock(block);
                
                propertyBlocks[data.renderer] = block;
            }
        }
    }
    
    // Обработка сигналов
    private void OnOutlineChangeSignal(OutlineChangeSignal signal)
    {
        SetObjectOutline(signal.TargetRenderer, signal.NewColor, signal.NewThickness, signal.EnableOutline);
    }
    
    private void OnGlobalOutlineChangeSignal(GlobalOutlineChangeSignal signal)
    {
        SetAllOutlines(signal.NewColor, signal.NewThickness, signal.EnableOutline);
    }
    
    // Публичные методы
    public void SetObjectOutline(Renderer renderer, Color color, float thickness, bool enable)
    {
        if (propertyBlocks.ContainsKey(renderer))
        {
            var block = propertyBlocks[renderer];
            
            if (enable)
            {
                block.SetColor(OutlineColorID, color);
                block.SetFloat(ThicknessID, thickness);
            }
            else
            {
                block.SetColor(OutlineColorID, Color.clear);
            }
            
            renderer.SetPropertyBlock(block);
        }
    }
    
    public void SetAllOutlines(Color color, float thickness, bool enable)
    {
        foreach (var kvp in propertyBlocks)
        {
            SetObjectOutline(kvp.Key, color, thickness, enable);
        }
    }
    
    private void OnDestroy()
    {
        if (signalBus != null)
        {
            signalBus.Unsubscribe<OutlineChangeSignal>(OnOutlineChangeSignal);
            signalBus.Unsubscribe<GlobalOutlineChangeSignal>(OnGlobalOutlineChangeSignal);
        }
    }
}*/  //TODO аутлайн менеджер получает запрос от вью на смену цвета
    }
}
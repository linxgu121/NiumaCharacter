namespace NiumaCharacter
{
    public interface ICharacterCatalogQuery
    {
        /// <summary>
        /// 获取角色模板信息
        /// </summary>
        bool TryGetDefinition(ushort characterId, out CharacterDefinitionSO definition);
    }
}

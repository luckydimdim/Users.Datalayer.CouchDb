namespace Cmas.DataLayers.CouchDb.Users
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя сущности
        /// </summary>
        public const string ServiceName = "users"; 

        /// <summary>
        /// Имя дизайн документа
        /// </summary>
        public const string DesignDocumentName = "users";

        /// <summary>
        /// Имя представления всех документов
        /// </summary>
        public const string AllDocsViewName = "all";

        /// <summary>
        /// Имя представления документов, сгрупированных по логину
        /// </summary>
        public const string ByLoginDocsViewName = "byLogin";
    }
}
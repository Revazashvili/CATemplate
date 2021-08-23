using System;

namespace Domain.Common
{
    public abstract class Auditable
    {
        /// <summary>
        /// Gets or sets the create time for this entity.
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Gets or sets the creator for this entity.
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the last modify time for this entity.
        /// </summary>
        public DateTime? LastModified { get; set; }
        /// <summary>
        /// Gets or sets the modifier for this entity.
        /// </summary>
        public string? LastModifiedBy { get; set; }
    }
}
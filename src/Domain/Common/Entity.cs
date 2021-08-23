namespace Domain.Common
{
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public long Id { get; set; }
    }
    public abstract class Entity : IEntity
    {
        /// <inheritdoc cref="IEntity.Id"/>
        public long Id { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="Entity"/>.
        /// </summary>
        protected Entity() { }

        /// <summary>
        /// Initializes a new instance of <see cref="Entity"/>.
        /// </summary>
        /// <param name="id">The primary key.</param>
        protected Entity(long id) { Id = id; }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
                return false;
            
            if (ReferenceEquals(this, other))
                return false;
            
            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }
        
        public static bool operator ==(Entity a, Entity b) => a.Equals(b);
        public static bool operator !=(Entity a, Entity b) => !(a == b);
    }
}
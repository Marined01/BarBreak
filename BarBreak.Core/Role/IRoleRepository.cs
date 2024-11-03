namespace BarBreak.Core.Role;

using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;

// using abstraction in Core layer is good in terms of the PURE Dependency Injection, you can replace adapted implementation (BarBreak.Infrastructure) on local, but it's not commonly decision
// for more information about PURE Di and about the difference between Pure and container - DI Principles, Practices and Patterns
public interface IRoleRepository : IRepository<int, RoleEntity>;
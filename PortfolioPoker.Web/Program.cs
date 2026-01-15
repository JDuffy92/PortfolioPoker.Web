using PortfolioPoker.Application.Interfaces;
using PortfolioPoker.Application.Services;
using PortfolioPoker.Domain.Interfaces;
using PortfolioPoker.Domain.Services;
using PortfolioPoker.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add Razor components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ----------------------------
// DOMAIN SERVICES (Singleton)
// ----------------------------
// These are stateless services or services that can safely live for the app's lifetime
builder.Services.AddSingleton<IHandEvaluator, PokerHandEvaluator>();
builder.Services.AddSingleton<IScoringService, ScoringService>();
builder.Services.AddSingleton<IRoundActionValidator, RoundActionValidator>();
builder.Services.AddSingleton<IRoundStateEvaluator, RoundStateEvaluator>();
builder.Services.AddSingleton<IRoundPhaseTransitionService, RoundPhaseTransitionService>();
builder.Services.AddSingleton<IRoundSnapshotService, RoundSnapshotService>();
builder.Services.AddSingleton<IRoundRewardService, RoundRewardService>();
builder.Services.AddSingleton<IRoundDescriptorFactory, RoundDescriptorFactory>();

// ----------------------------
// APPLICATION SERVICES
// ----------------------------
// These depend on domain services and can also be singletons if they are stateless
builder.Services.AddSingleton<IRunSetupService>(sp =>
    new RunSetupService(
        sp.GetRequiredService<IRoundDescriptorFactory>(),
        sp.GetRequiredService<IRoundRewardService>()
    )
);

builder.Services.AddSingleton<IRunProgressService>(sp =>
    new RunProgressService(
        new RoundSetupService(
            sp.GetRequiredService<IHandEvaluator>(),
            sp.GetRequiredService<IScoringService>()
        ),
        sp.GetRequiredService<IRoundRewardService>()
    )
);

builder.Services.AddSingleton<IGameRoundService>(sp =>
    new GameRoundService(
        sp.GetRequiredService<IHandEvaluator>(),
        sp.GetRequiredService<IRoundActionValidator>(),
        sp.GetRequiredService<IRoundStateEvaluator>(),
        sp.GetRequiredService<IRoundPhaseTransitionService>(),
        sp.GetRequiredService<IRoundSnapshotService>(),
        sp.GetRequiredService<IScoringService>()
    )
);

builder.Services.AddScoped<IRunStateService, RunStateService>();

builder.Services.AddSingleton<IAnimationCoordinator, AnimationCoordinator>();


var app = builder.Build();

// ----------------------------
// HTTP Request Pipeline
// ----------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

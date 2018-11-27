using AutoHome.Training.Dapper;
using AutoHome.Training.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AutoHome.Training.Core.UnitOfWork;

namespace AutoHome.Training.Repository
{
    public class ProductRepository: TrainingRepositoryBase<ProductInfo,int>
    {
        private readonly TraingRepositoryContext _traingRepositoryContext;
        private readonly IDapperRepository<ProductDetailInfo> _dapperRepository;
        public ProductRepository(
            TraingRepositoryContext traingRepositoryContextBase,
            IDapperRepository<ProductDetailInfo> dapperRepository
            ) : base(traingRepositoryContextBase)
        {
            _traingRepositoryContext = traingRepositoryContextBase;
            _dapperRepository = dapperRepository;
        }

        public async Task<int> AddProductAndDetail(ProductInfo productInfo,List<ProductDetailInfo> productDetailInfos)
        {
            var productId = 0;
            using (IUnitOfWork uow = new DapperUnitOfWork(_traingRepositoryContext))
            {
                productId = await Connection.InsertAsync(productInfo) ?? 0;
                foreach (var productDetailInfo in productDetailInfos)
                {
                    productDetailInfo.ProductId = productId;
                    await _dapperRepository.InsertAsync(productDetailInfo);
                }
                uow.SaveChanges();
            }
            return productId;
        }
    }
}
